using Application.People.Command.CreatePerson;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class PersonController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonCommand command)
    {
        var result = await Mediator.Send(command);
        return result.IsFailed ? BadRequest(result.ToActionResult()) : Ok(result.ToActionResult());
    }
}