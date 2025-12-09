using ApplicationLayer.Features.Authorize.Commands.Register;
using ApplicationLayer.Features.Authorize.DTOs;
using ApplicationLayer.Features.Authorize.Queries.Login;
using ApplicationLayer.Features.User.Queries.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            // Create command from the received DTO
            var registerCommand = new RegisterCommand(
                userName: userRegisterDto.UserName,
                firstName: userRegisterDto.FirstName,
                lastName: userRegisterDto.LastName,
                phoneNumber: userRegisterDto.PhoneNumber,
                userEmail: userRegisterDto.UserEmail,
                password: userRegisterDto.Password,
                role: userRegisterDto.Role
            );

            // Execute command
            var result = await _mediator.Send(registerCommand);

            // Return response
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var query = new LoginQuery(userLoginDto.UserName, userLoginDto.Password);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            return Ok(result);
        }

    }
}
