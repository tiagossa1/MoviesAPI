using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Movies.Command.CreateMovie;

public record CreateMovieCommand(string Title, decimal Budget, string HomepageUrl, string Plot, DateTime ReleaseDate, int RuntimeInMinutes, List<long> GenreIds, List<CreateMovieCastsCommand> MovieCasts) : IRequest<Result<long>>;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<long>>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<long>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Budget = request.Budget,
            Homepage = request.HomepageUrl,
            Plot = request.Plot,
            ReleaseDate = request.ReleaseDate,
            RuntimeInMinutes = request.RuntimeInMinutes,
            Genres = request.GenreIds
                .Select(genreId => new Genre
                {
                    Id = genreId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                })
                .ToList(),
            MovieCasts = request
                .MovieCasts
                .Select(command => new MovieCast
                {
                    CharacterName = command.CharacterName,
                    PersonId = command.PersonId,
                    GenderId = command.GenderId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                })
                .ToList()
        };

        var result = await _movieRepository.Create(movie);
        return Result.Ok(result.Id);
    }
}