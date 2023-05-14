using Application.Genders.Queries.GetGenders;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetGendersQuery());
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }
    }
}
