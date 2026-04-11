using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

/// <summary>
/// Provides authentication and user account management endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">Service responsible for authentication and user operations.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user and returns an access token.
    /// </summary>
    /// <param name="request">User login credentials.</param>
    /// <returns>
    /// Authentication response containing access token and user details.
    /// </returns>
    /// <response code="200">Login successful.</response>
    /// <response code="401">Invalid credentials.</response>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null)
            return Unauthorized("Invalid email or password.");

        return Ok(result);
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="request">User registration data.</param>
    /// <returns>
    /// Authentication response after successful registration.
    /// </returns>
    /// <response code="200">User successfully registered.</response>
    /// <response code="400">Invalid registration data.</response>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (result == null)
            return BadRequest("Registration failed.");

        return Ok(result);
    }

    /// <summary>
    /// Changes the password of the authenticated user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="passwordUpdate">Request containing current and new password.</param>
    /// <remarks>
    /// Users can only change their own password.
    /// </remarks>
    /// <returns>
    /// Authentication response indicating the result of the operation.
    /// </returns>
    /// <response code="200">Password successfully changed.</response>
    /// <response code="401">Unauthorized access.</response>
    [HttpPut("{id:guid}/password")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> ChangePassword(Guid id, PasswordUpdate passwordUpdate)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != id.ToString())
            return Unauthorized("You can only change your own password.");

        var result = await _authService.PasswordChangeAsync(id, passwordUpdate);

        if (result == null)
            return BadRequest("Password change failed.");

        return Ok(result);
    }

    /// <summary>
    /// Updates the profile information of the authenticated user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="request">Updated user information.</param>
    /// <remarks>
    /// Users can only update their own account.
    /// </remarks>
    /// <returns>
    /// Authentication response with updated user data.
    /// </returns>
    /// <response code="200">User successfully updated.</response>
    /// <response code="401">Unauthorized access.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> Update(Guid id, UpdateRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != id.ToString())
            return Unauthorized("You can only update your own account.");

        var result = await _authService.UpdateAsync(id, request);

        if (result == null)
            return BadRequest("Update failed.");

        return Ok(result);
    }

    /// <summary>
    /// Deletes the authenticated user's account.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <remarks>
    /// Users can only delete their own account.
    /// </remarks>
    /// <returns>
    /// No content if deletion is successful.
    /// </returns>
    /// <response code="204">User successfully deleted.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="404">User not found or cannot be deleted.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != id.ToString())
            return Unauthorized("You can only delete your own account.");

        var result = await _authService.DeleteAsync(id);

        if (!result)
            return NotFound("User not found or could not be deleted.");

        return NoContent();
    }

    /// <summary>
    /// Refreshes the access token using a valid refresh token.
    /// </summary>
    /// <param name="request">Request containing the refresh token.</param>
    /// <returns>
    /// A new authentication response with refreshed tokens.
    /// </returns>
    /// <response code="200">Token successfully refreshed.</response>
    /// <response code="401">Invalid or expired refresh token.</response>
    [HttpPost("refreshToken")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request);

        if (result == null)
            return Unauthorized("Invalid or expired refresh token.");

        return Ok(result);
    }

    /// <summary>
    /// Retrieves user profile information by user identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>
    /// The user's profile information.
    /// </returns>
    /// <response code="200">User information retrieved successfully.</response>
    /// <response code="404">User not found.</response>
    [HttpPost("{id:guid}/user")]
    public async Task<ActionResult<UserResponseDto>> GetUserInfo(Guid id)
    {
        var user = await _authService.GetUserInfoAsync(id);

        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }
}