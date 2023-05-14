using Application.Movies.Command.CreateMovie;
using Application.Movies.Command.DeleteMovie;
using Application.Movies.Queries.GetMovies;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MoviesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetMoviesQuery());
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return result.IsFailed ? Problem() : Ok();
        }
    }
}
