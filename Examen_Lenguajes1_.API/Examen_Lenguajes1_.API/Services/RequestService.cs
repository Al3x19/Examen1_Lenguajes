using AutoMapper;
using Azure;
using Examen_Lenguajes1_.API.Dtos.Requests;
using Examen_Lenguajes1_.API.Database;
using Examen_Lenguajes1_.API.Database.Entities;
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Examen_Lenguajes1_.API.Services
{
    public class RequestService : IRequestService
    {
        private readonly Examen_Lenguajes1_Context _context;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly int PAGE_SIZE;
        private readonly UserManager<EmployeeEntity> _userManager;

        RequestService(UserManager<EmployeeEntity> userManager, 
                       Examen_Lenguajes1_Context context, 
                       IMapper mapper, 
                       IConfiguration configuration,
                       IAuditService auditService) 

        {
            this._userManager = userManager;
            this._context = context;
            this._mapper = mapper;
            this._auditService = auditService;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<RequestDto>> Create(RequestCreateDto dto)
        {
            var requestEntity = _mapper.Map<RequestEntity>(dto);
            if (requestEntity.SubmitDate<DateTime.Now || requestEntity.SubmitDate<requestEntity.EndDate.AddDays(1) || (requestEntity.EndDate-requestEntity.SubmitDate).TotalDays>30)
            {
                return new ResponseDto<RequestDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Las fechas no son validas",
                };
            }



            requestEntity.State = "En espera";

            _context.Requests.Add(requestEntity);

            await _context.SaveChangesAsync();

            var requestDto = _mapper.Map<RequestDto>(requestEntity);

            

            return new ResponseDto<RequestDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Creada exitosamente",
                Data = requestDto
            };
        }

        public async Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetRequestByIdAsync(int page = 1)
        {
           var identificacion = _auditService.GetUserId();
            int startIndex = (page - 1) * PAGE_SIZE;

            var RequestsEntityQuery = _context.Requests
                .Where(x => x.Id.Equals(identificacion));

            int totalRequests = await RequestsEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRequests / PAGE_SIZE);

            var requestsEntity = await RequestsEntityQuery
                .OrderBy(u => u.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var requestsDtos = _mapper.Map<List<RequestDto>>(requestsEntity);

            return new ResponseDto<PaginationDto<List<RequestDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Se encontraron las peticiones",
                Data = new PaginationDto<List<RequestDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalRequests,
                    TotalPages = totalPages,
                    Items = requestsDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<PaginationDto<List<RequestDto>>>> GetRequestsListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var requestsEntityQuery = _context.Requests
                .Where(x => x.CreatedBy.ToLower().Contains(searchTerm.ToLower()));

            int totalRequests = await requestsEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRequests / PAGE_SIZE);

            var requestsEntity = await requestsEntityQuery
                .OrderBy(u => u.CreatedDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var requestsDtos = _mapper.Map<List<RequestDto>>(requestsEntity);

            return new ResponseDto<PaginationDto<List<RequestDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de solicitudes encontrada",
                Data = new PaginationDto<List<RequestDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalRequests,
                    TotalPages = totalPages,
                    Items = requestsDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

    }
}
