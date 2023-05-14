using Application.Common.Dtos;
using Application.Common.Mappers;
using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Movies.Queries.GetMovies;

public record GetMoviesQuery : IRequest<Result<IEnumerable<MovieDto>>>;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, Result<IEnumerable<MovieDto>>>
{
    private readonly ILogger<GetMoviesQueryHandler> _logger;
    private readonly IMovieRepository _movieRepository;

    public GetMoviesQueryHandler(IMovieRepository movieRepository, ILogger<GetMoviesQueryHandler> logger)
    {
        _movieRepository = movieRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<MovieDto>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var movies = await _movieRepository.GetAll();
            if (!movies.Any())
            {
                return Result.Ok();
            }

            return Result.Ok(movies.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was an error while retrieving movies");
            return Result.Fail(e.Message);
        }
    }
}