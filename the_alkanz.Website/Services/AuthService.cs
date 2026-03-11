using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public class AuthService : IAuthService
{
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> PasswordChangeAsync(Guid id, PasswordUpdate passwordUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> UpdateAsync(Guid id, UpdateRequest updateRequest)
    {
        throw new NotImplementedException();
    }
}
