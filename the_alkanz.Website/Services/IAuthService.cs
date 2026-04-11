using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequest  refreshTokenRequest);
  
    Task<AuthResponseDto> UpdateAsync(Guid id, UpdateRequest updateRequest);
    Task<AuthResponseDto> PasswordChangeAsync(Guid id, PasswordUpdate passwordUpdate);
    Task<UserResponseDto> GetUserInfoAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
