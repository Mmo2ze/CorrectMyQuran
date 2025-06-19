using CorectMyQuran.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorectMyQuran.Features.Admins;

[Route("admin")]
[Authorize(Roles = "Admin")]
public class AdminController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateSheikh([FromBody] CreateSheikhCommand command)
    {
        var result = await sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(result.Value),
            Problem
        );
    }
    [HttpGet("sheikhs")]
    public async Task<IActionResult> GetSheikhs([FromQuery] GetSheikhsQuery query)
    {
        var result = await sender.Send(query);

        return result.Match<IActionResult>(
            _ => Ok(result.Value),
            Problem
        );
    }
}