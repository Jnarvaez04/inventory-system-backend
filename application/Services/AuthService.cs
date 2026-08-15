using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace inventarySystem_backend.application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        // Validar si el usuario o email ya están registrados
        var existingUser = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
        if(existingUser != null) return null;

        var existingEmail = await _unitOfWork.Users.GetEmailAsync(dto.Email);
        if(existingEmail != null) return null;


        // Hashear contraseña y asignar rol por regla de negocio
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Employee", 
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // generar respuesta con token
        return GenerateAuthResponse(user);
    }


    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        // buscar usuario por por email
        var user = await _unitOfWork.Users.GetEmailAsync(dto.Email);

        if(user == null) return null;

        // verificar contraseña con bcrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if(!isPasswordValid) return null;

        // generar token de sesión
        return GenerateAuthResponse(user);

    }

    private AuthResponseDto GenerateAuthResponse(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var expires = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["DurationInHours"] ?? "8"));

        // Claims: Información que viaja incrustada y firmada dentro del token
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(tokenDescriptor);

        return new AuthResponseDto
        {
          Token = tokenString,
          Username = user.Username,
          Role = user.Role,
          Expiration = expires  
        };
    }
}