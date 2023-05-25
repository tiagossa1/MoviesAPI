using Application.Interfaces;
using FluentResults;
using MediatR;

namespace Application.Movies.Command.DeleteMovie;

public record DeleteMovieCommand(long Id) : IRequest<Result>;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Result>
{
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var result = await _movieRepository.Delete(request.Id);
        return result ? Result.Ok() : Result.Fail(new Error($"Could not delete movie with id {request.Id}"));
    }
}