using Application.Genres.Queries.GetGenres;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class GenresController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetGenresQuery());
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }
    }
}
