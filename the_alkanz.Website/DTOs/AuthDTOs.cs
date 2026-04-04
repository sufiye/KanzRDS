namespace the_alkanz.Website.DTOs;

public class RegisterRequest
{
    /// <summary>
    /// User's first name.
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// User's last name.
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// User's address (optional).
    /// </summary>
    /// <example>123 Main St, Baku, Azerbaijan</example>
    public string? Address { get; set; }

    /// <summary>
    /// User's phone number (optional).
    /// </summary>
    /// <example>+994501234567</example>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// User's email address.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// </summary>
    /// <example>P@ssw0rd!</example>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation of the password.
    /// </summary>
    /// <example>P@ssw0rd!</example>
    public string ConfirmedPassword { get; set; } = string.Empty;
}

public class UpdateRequest
{
    /// <summary>
    /// Updated first name.
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Updated last name.
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Updated address (optional).
    /// </summary>
    /// <example>123 Main St, Baku, Azerbaijan</example>
    public string? Address { get; set; }

    /// <summary>
    /// Updated phone number (optional).
    /// </summary>
    /// <example>+994501234567</example>
    public string? PhoneNumber { get; set; }
}

public class PasswordUpdate
{
    /// <summary>
    /// Current password of the user.
    /// </summary>
    /// <example>P@ssw0rd!</example>
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// New password.
    /// </summary>
    /// <example>N3wP@ssw0rd!</example>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation of the new password.
    /// </summary>
    /// <example>N3wP@ssw0rd!</example>
    public string ConfirmedPassword { get; set; } = string.Empty;
}

public class LoginRequest
{
    /// <summary>
    /// User's email address.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// </summary>
    /// <example>P@ssw0rd!</example>
    public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    /// <summary>
    /// User's email address.
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// JWT access token for authentication.
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Expiration date and time of the access token.
    /// </summary>
    /// <example>2026-03-31T12:00:00Z</example>
    public DateTime ExpiredAt { get; set; }

    /// <summary>
    /// List of roles assigned to the user.
    /// </summary>
    /// <example>[ "Admin", "User" ]</example>
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}

public class RefreshTokenRequest
{
    /// <summary>
    /// The refresh token previously issued to the user.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public string RefreshToken { get; set; } = string.Empty;
}