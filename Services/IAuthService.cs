// Services/IUserService.cs
using System.Threading.Tasks;
using Greenhouse.DTOs;
using Greenhouse.Response;

namespace Greenhouse.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterModel model);
        Task<AuthResult> LoginAsync(LoginModel model);
        Task<AuthResult> SendResetCodeAsync(SendResetCodeModel model);
        Task<AuthResult> VerifyResetCodeAsync(VerifyResetCodeModel model);
        Task<AuthResult> UpdatePasswordAsync(UpdatePasswordModel model);



        
    }
}
