// Controllers/AuthController.cs
using Greenhouse.DTOs;
using Greenhouse.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;

        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(model);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginAsync(model);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("send-reset-code")]
        public async Task<IActionResult> SendResetCode([FromBody] SendResetCodeModel model)
        {
            var result = await _userService.SendResetCodeAsync(model);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeModel model)
        {
            var result = await _userService.VerifyResetCodeAsync(model);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("update-password")]
public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await _userService.UpdatePasswordAsync(model);
    if (!result.Success)
        return BadRequest(result);

    return Ok(result);
}



    }
}
