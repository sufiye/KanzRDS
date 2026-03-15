using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    /// <summary>
    /// Authenticates a user and returns an access token.
    /// </summary>
    /// <param name="loginRequest">User login credentials.</param>
    /// <returns>Authentication response containing token and user information.</returns>
    /// <response code="200">Returns authentication result.</response>
    /// <response code="401">Invalid credentials.</response>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequest loginRequest)
    {
        var result = await _authService.LoginAsync(loginRequest);
        return Ok(result);
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="registerRequest">User registration information.</param>
    /// <returns>Authentication response after successful registration.</returns>
    /// <response code="200">User successfully registered.</response>
    /// <response code="400">Invalid registration data.</response>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequest registerRequest)
    {
        var result = await _authService.RegisterAsync(registerRequest);
        return Ok(result);
    }

    /// <summary>
    /// Changes the password of an authenticated user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="passwordUpdate">Password update request containing old and new password.</param>
    /// <returns>Authentication response indicating the result.</returns>
    /// <response code="200">Password successfully changed.</response>
    /// <response code="401">Unauthorized request.</response>
    [HttpPut("{id:guid}/passwordChange")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> PasswordChange(Guid id, PasswordUpdate passwordUpdate)
    {
        var result = await _authService.PasswordChangeAsync(id, passwordUpdate);
        return Ok(result);
    }

    /// <summary>
    /// Updates the profile information of the authenticated user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="updateRequest">Updated user information.</param>
    /// <returns>Authentication response indicating the result.</returns>
    /// <response code="200">User successfully updated.</response>
    /// <response code="401">Unauthorized if the user tries to update another account.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> Update(Guid id, UpdateRequest updateRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != id.ToString())
            return Unauthorized();

        var result = await _authService.UpdateAsync(id, updateRequest);

        return Ok(result);
    }

    /// <summary>
    /// Deletes the authenticated user's account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">User successfully deleted.</response>
    /// <response code="401">Unauthorized if the user tries to delete another account.</response>
    /// <response code="404">User not found or cannot be deleted.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != id.ToString())
            return Unauthorized();

        var result = await _authService.DeleteAsync(id);

        if (result == false) return NotFound("Can not delete this user");

        return NoContent();
    }
}
