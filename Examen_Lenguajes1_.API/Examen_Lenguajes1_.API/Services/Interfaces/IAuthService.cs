using Examen_Lenguajes1_.API.Dtos.Auth;
using Examen_Lenguajes1_.API.Dtos.Common;

namespace Examen_Lenguajes1_.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
    }
}
