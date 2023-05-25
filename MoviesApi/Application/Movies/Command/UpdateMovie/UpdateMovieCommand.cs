using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Movies.Command.UpdateMovie;

public record UpdateMovieCommand(long Id, string Title, decimal Budget, string HomepageUrl, string Plot, DateTime ReleaseDate, int RuntimeInMinutes, List<UpdateMovieCastsCommand> MovieCasts, List<long> Genres) : IRequest<Result>;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Result>
{
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var result = await _movieRepository.Update(new Movie
        {
            Homepage = request.HomepageUrl,
            Budget = request.Budget,
            Genres = request.Genres
                .Select(genreId => new Genre
                {
                    Id = genreId
                })
                .ToList(),
            MovieCasts = request.MovieCasts
                .Select(command => new MovieCast
                {
                    GenderId = command.GenderId,
                    CharacterName = command.CharacterName,
                    PersonId = command.PersonId,
                    MovieId = request.Id,
                    UpdatedAt = DateTime.UtcNow
                })
                .ToList(),
            ReleaseDate = request.ReleaseDate,
            RuntimeInMinutes = request.RuntimeInMinutes,
            Id = request.Id,
            Plot = request.Plot,
            Title = request.Title,
            UpdatedAt = DateTime.UtcNow
        });

        return result ? Result.Ok() : Result.Fail(new Error($"Could not update movie with id {request.Id}"));
    }
}