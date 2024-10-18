using Examen_Lenguajes1_.API.Constants;
using Examen_Lenguajes1_.API.Database.Entities;
using Examen_Lenguajes1_.API.Dtos.Requests;
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen_Lenguajes1_.API.Controllers
{
    [ApiController]
    [Route("api/requests")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RequestsController :ControllerBase
    {
        private readonly IRequestService _requestsService;

        public RequestsController(IRequestService requestsService)
        {
            this._requestsService = requestsService;
        }

        [HttpGet]
        [Authorize(Roles = $" {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<List<RequestDto>>>> GetAll(
            string searchTerm = "", 
            int page = 1) 
        {
            var response = await _requestsService.GetRequestsListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<RequestDto>>> Get(Guid id) 
        {
            var response = await _requestsService.GetRequestByIdAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]

        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}, {RolesConstant.USER}")]
        public async Task<ActionResult<ResponseDto<RequestDto>>> Create(RequestCreateDto dto)
        {
            var response = await _requestsService.Create(dto);

            return StatusCode(response.StatusCode, response);
        }



    }
}
