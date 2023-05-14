using Application.Common.Dtos;
using Application.Common.Mappers;
using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Genres.Queries.GetGenres;

public record GetGenresQuery : IRequest<Result<IEnumerable<GenreDto>>>;

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, Result<IEnumerable<GenreDto>>>
{
    private readonly ILogger<GetGenresQueryHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetGenresQueryHandler(ILogger<GetGenresQueryHandler> logger, IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }

    public async Task<Result<IEnumerable<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var genres = await _genreRepository.GetAll();
            return Result.Ok(genres.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was an error retrieving the genres");
            return Result.Fail(e.Message);
        }
    }
}