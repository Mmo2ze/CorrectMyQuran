using CorectMyQuran.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorectMyQuran.Features.Auth
{
    [Route("auth")]
    public class AuthController(ISender mediator) : ApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var result = await mediator.Send(request);

            return result.Match<IActionResult>(
                _ => Ok(result.Value),
                Problem
            );
        }
        
        [HttpGet("hmm")]
        [Authorize(Roles = "Admin")]
        public IActionResult Hmm()
        {
            return Ok("You are authorized as Admin");
        }
    }
    
}