using System.Data;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly string _connectionString;
    private readonly IPersonRepository _personRepository;
    private readonly IGenderRepository _genderRepository;

    private const string GetMoviesCastsByMovieIds = @"SELECT
                                                                MovieId,
                                                                GenderId,
                                                                PersonId,
                                                                CharacterName,
                                                                CreatedAt,
                                                                UpdatedAt
                                                            FROM
                                                                MoviesCast
                                                            WHERE
                                                                MovieId IN @movieIds;
";

    public MovieRepository(IConfiguration configuration, IPersonRepository personRepository,
        IGenderRepository genderRepository)
    {
        _personRepository = personRepository;
        _genderRepository = genderRepository;
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IList<Movie>> GetAll()
    {
        const string sql = @"SELECT
	                            M.*,
	                            MC.PersonId,
	                            MC.MovieId,
	                            MC.GenderId,
	                            MC.CharacterName,
	                            GR.Id,
	                            GR.Name,
	                            G.Id,
	                            G.Name,
	                            P.Id,
	                            P.Name
                            FROM
                                Movies M 
                            LEFT JOIN MoviesGenres MG ON M.Id = MG.MovieId
                            LEFT JOIN MoviesCast MC ON MC.MovieId = M.Id
                            LEFT JOIN Genres GR ON GR.Id = MG.GenreId
                            LEFT JOIN Genders G ON G.Id = MC.GenderId
                            LEFT JOIN People P ON P.Id = MC.PersonId";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var movieDictionary = new Dictionary<long, Movie>();

        var movies = connection.Query<Movie, MovieCast, Genre, Gender, Person, Movie>(sql,
            (movie, movieCast, genre, gender, person) =>
            {
                if (!movieDictionary.TryGetValue(movie.Id, out var mappedMovie))
                {
                    mappedMovie = movie;
                    mappedMovie.MovieCasts ??= new List<MovieCast>();
                    mappedMovie.Genres ??= new List<Genre>();

                    movieDictionary.Add(mappedMovie.Id, mappedMovie);
                }

                if (movieCast != null && mappedMovie.MovieCasts.All(c => c.PersonId != movieCast.PersonId))
                {
                    movieCast.Person = person; 
                    movieCast.Gender ??= gender;
                    
                    mappedMovie.MovieCasts.Add(movieCast);
                }

                if (genre != null && mappedMovie.Genres.All(g => g.Id != genre.Id))
                {
                    mappedMovie.Genres.Add(genre);
                }

                return mappedMovie;
            }, splitOn: "PersonId, Id, Id, Id", commandType: CommandType.Text, buffered: true);

        return movies.Distinct().ToList();
    }

    public async Task<Movie> GetById(long id)
    {
        const string sql = @"SELECT
                                    *
                            FROM
                                Movies
                            WHERE Id = @id";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var movie = await connection.QueryFirstOrDefaultAsync<Movie>(sql, new { id });
        var movieCast = await connection.QueryAsync<MovieCast>(GetMoviesCastsByMovieIds, new
        {
            movieIds = new[] { movie.Id }
        });

        var personIds = movieCast
            .Select(mc => mc.PersonId)
            .ToList();

        var genderIds = movieCast
            .Select(mc => mc.GenderId)
            .Distinct()
            .ToList();

        var people = await _personRepository.GetByIds(personIds);
        var genders = await _genderRepository.GetByIds(genderIds);

        movie.MovieCasts = new List<MovieCast>();

        foreach (var mc in movieCast)
        {
            mc.Gender = genders.FirstOrDefault(gender => gender.Id == mc.GenderId);
            mc.Person = people.FirstOrDefault(person => person.Id == mc.PersonId);
        }

        return movie;
    }

    public async Task<Movie> Create(Movie obj)
    {
        const string sql =
            @"INSERT INTO Movies(Title, Budget, Homepage, Plot, ReleaseDate, RuntimeInMinutes, CreatedAt, UpdatedAt) VALUES(@title, @budget, @homepage, @plot, @releaseDate, @runtimeInMinutes, @createdAt, @updatedAt);
         SELECT last_insert_rowid()";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        var movieId = await connection.ExecuteScalarAsync<long>(sql,
            new
            {
                title = obj.Title,
                budget = obj.Budget,
                homepage = obj.Homepage,
                plot = obj.Plot,
                releaseDate = obj.ReleaseDate,
                runtimeInMinutes = obj.RuntimeInMinutes,
                createdAt = obj.CreatedAt,
                updatedAt = obj.UpdatedAt
            });

        foreach (var movieCast in obj.MovieCasts)
        {
            await connection.ExecuteAsync(
                @"INSERT INTO MoviesCast(MovieId, GenderId, PersonId, CharacterName, CreatedAt, UpdatedAt) VALUES (@movieId, @genderId, @personId, @characterName, @createdAt, @updatedAt)",
                new
                {
                    movieId,
                    genderId = movieCast.GenderId,
                    personId = movieCast.PersonId,
                    characterName = movieCast.CharacterName,
                    createdAt = movieCast.CreatedAt,
                    updatedAt = movieCast.UpdatedAt
                });
        }

        foreach (var genre in obj.Genres)
        {
            await connection.ExecuteAsync(
                @"INSERT INTO MoviesGenres(MovieId, GenreId, CreatedAt, UpdatedAt) VALUES (@movieId, @genreId, @createdAt, @updatedAt)",
                new
                {
                    movieId,
                    genreId = genre.Id,
                    createdAt = genre.CreatedAt,
                    updatedAt = genre.UpdatedAt
                });
        }

        await transaction.CommitAsync();

        return await GetById(movieId);
    }

    public async Task<bool> Update(Movie obj)
    {
        const string sql = @"UPDATE Movies
                            SET Title = @title, Budget = @budget, Homepage = @homepage, Plot = @plot, ReleaseDate = @releaseDate, RuntimeInMinutes = @runtimeInMinutes, UpdatedAt = @updatedAt
                            WHERE Id = @id";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(sql, new
        {
            id = obj.Id,
            title = obj.Title, budget = obj.Budget, homepage = obj.Homepage, plot = obj.Plot,
            releaseDate = obj.ReleaseDate, runtimeInMinutes = obj.RuntimeInMinutes,
            updatedAt = obj.UpdatedAt
        });

        return true;
    }

    public async Task<bool> Delete(long id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var transaction = await connection.BeginTransactionAsync();

        await connection.ExecuteAsync(@"DELETE FROM MoviesCast WHERE MovieId = @movieId", new { movieId = id });
        await connection.ExecuteAsync(@"DELETE FROM MoviesGenres WHERE MovieId = @movieId", new {  movieId = id });
        await connection.ExecuteAsync(@"DELETE FROM Movies WHERE Id = @id", new { id });
        
        await transaction.CommitAsync();

        return true;
    }
}