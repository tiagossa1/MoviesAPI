using Application.Common.Dtos;
using Application.Common.Mappers;
using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Genders.Queries.GetGenders;

public record GetGendersQuery : IRequest<Result<IList<GenderDto>>>;

public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, Result<IList<GenderDto>>>
{
    private readonly ILogger<GetGendersQueryHandler> _logger;
    private readonly IGenderRepository _genderRepository;

    public GetGendersQueryHandler(ILogger<GetGendersQueryHandler> logger, IGenderRepository genderRepository)
    {
        _logger = logger;
        _genderRepository = genderRepository;
    }

    public async Task<Result<IList<GenderDto>>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var genders = await _genderRepository.GetAll();
            return Result.Ok(genders.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was an error retrieving the genders");
            return Result.Fail(e.Message);
        }
    }
}