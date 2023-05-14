using Application.People.Command.CreatePerson;
using Application.People.Queries.GetPeople;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class PeopleController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetPeopleQuery());
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command)
        {
            var result = await Mediator.Send(command);
            return result.IsFailed ? Problem() : Ok(result.ToActionResult());
        }
    }
}
