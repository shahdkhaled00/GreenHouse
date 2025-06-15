using Greenhouse.DbContexts;
using Greenhouse.DTOs;
using Greenhouse.Models;
using Greenhouse.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Greenhouse.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AuthService(AppDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _config = configuration;
            _emailService = emailService;
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            // Check if email is already taken
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return new AuthResult { Success = false, Message = "Email is already registered." };
            }

            var user = new User
            {
                Email = model.Email
            };
            user.SetPassword(model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResult { Success = true, Message = "User registered successfully.", UserId = user.Id };
        }
        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !user.VerifyPassword(model.Password))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // Generate JWT
            var token = GenerateJwtToken(user);

            return new AuthResult
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                UserId = user.Id
            };
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //reset password
        public async Task<AuthResult> SendResetCodeAsync(SendResetCodeModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return new AuthResult { Success = false, Message = "Email not found." };

            var code = new Random().Next(100000, 999999).ToString();
            user.ResetCode = code;
            user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(10);

            await _context.SaveChangesAsync();

            // Send Email
            await _emailService.SendEmailAsync(user.Email, "Reset Password Code", $"Your code is: {code}");

            return new AuthResult { Success = true, Message = "Reset code sent to email." };
        }

        public async Task<AuthResult> VerifyResetCodeAsync(VerifyResetCodeModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || user.ResetCode != model.Code || user.ResetCodeExpiry < DateTime.UtcNow)
            {
                return new AuthResult { Success = false, Message = "Invalid or expired code." };
            }

            return new AuthResult { Success = true, Message = "Code verified." };
        }

        public async Task<AuthResult> UpdatePasswordAsync(UpdatePasswordModel model)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
    if (user == null)
        return new AuthResult { Success = false, Message = "User not found." };

    user.SetPassword(model.NewPassword);
    user.ResetCode = null;
    user.ResetCodeExpiry = null;

    await _context.SaveChangesAsync();

    return new AuthResult { Success = true, Message = "Password updated successfully." };
}




    }
}
