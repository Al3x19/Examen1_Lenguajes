
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Examen_Lenguajes1_.API.Services.Interfaces
{
    public interface IRequestService
    {
        
        Task<ResponseDto<RequestDto>> Create(RequestCreateDto dto);
        Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetRequestByIdAsync(int page = 1);
        Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetRequestsListAsync(string searchTerm = "", int page = 1);
    }
}
