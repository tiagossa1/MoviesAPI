using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Movies.Command.UpdateMovie;

public record UpdateMovieCommand(long Id, string Title, decimal Budget, string HomepageUrl, string Plot, DateTime ReleaseDate, int RuntimeInMinutes, IEnumerable<UpdateMovieCastsCommand> MovieCasts, IEnumerable<UpdateMovieGenreCommand> Genres) : IRequest<Result>;

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
            HomepageUrl = request.HomepageUrl,
            Budget = request.Budget,
            Genres = request.Genres.Select(command => new Genre
            {
                Id = command.Id
            }),
            MovieCasts = request.MovieCasts.Select(command => new MovieCast
            {
                GenderId = command.GenderId,
                CharacterName = command.CharacterName,
                PersonId = command.PersonId,
                MovieId = request.Id,
                UpdatedAtUtc = DateTime.UtcNow
            }),
            ReleaseDate = request.ReleaseDate,
            RuntimeInMinutes = request.RuntimeInMinutes,
            Id = request.Id,
            Plot = request.Plot,
            Title = request.Title,
            UpdatedAtUtc = DateTime.UtcNow
        });

        return result ? Result.Ok() : Result.Fail(new Error($"Could not update movie with id {request.Id}"));
    }
}