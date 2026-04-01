using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService
        (UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {

        var deleteUser = await _userManager
                                    .Users
                                    .FirstOrDefaultAsync(u => u.Id == id.ToString());

        if (deleteUser is null)
            return false;

        await _userManager.DeleteAsync(deleteUser);

        return true;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null)
        {
            throw new InvalidOperationException("Invalid password or email");
        }

        var invalidPassword = await _userManager.CheckPasswordAsync(user!, loginRequest.Password);

        if (!invalidPassword)
        {
            throw new InvalidOperationException("Invalid password or email");
        }

        return await CreatTokenAsync(user);

    }

    public async Task<AuthResponseDto> PasswordChangeAsync(Guid id, PasswordUpdate passwordUpdate)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new InvalidOperationException("Invalid password or email");
        }

        var result = await _userManager.ChangePasswordAsync(
        user!,
        passwordUpdate.CurrentPassword,
        passwordUpdate.Password
         );

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed : {errors}");
        }


        return _mapper.Map<AuthResponseDto>(user);


    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var user = await _userManager.Users.
                            FirstOrDefaultAsync(r => r.RefreshToken == refreshTokenRequest.RefreshToken);
        if (user == null)
                         return null!;

        return await CreatTokenAsync(user);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("This user alredy exists");
        }

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Address = registerRequest.Address,
            PhoneNumber = registerRequest.PhoneNumber,
            Email = registerRequest.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed : {errors}");
        }

        return await CreatTokenAsync(user);

    }

    public async Task<AuthResponseDto> UpdateAsync(Guid id, UpdateRequest updateRequest)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        user.FirstName = updateRequest.FirstName;
        user.LastName = updateRequest.LastName;
        user.Address = updateRequest.Address;
        user.PhoneNumber = updateRequest.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed : {errors}");
        }

        return _mapper.Map<AuthResponseDto>(user);


    }
    private async Task<AuthResponseDto> CreatTokenAsync(ApplicationUser user)
    {

        var jwtSettings = _configuration.GetSection("JWTSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expirationInMinutes = int.Parse(jwtSettings["ExpirationInMinutes"]!);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        var credetials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Email,user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationInMinutes),
            signingCredentials: credetials
            );

        var refreshToken = Guid.NewGuid().ToString("N").ToLower();
        user.RefreshToken = refreshToken;

        await _userManager.UpdateAsync(user);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDto
        {
            AccessToken = tokenString,
            RefreshToken = refreshToken,
            ExpiredAt = DateTime.UtcNow.AddMinutes(expirationInMinutes),
            Email = user.Email!,
            Roles = roles
        };
    }


}
