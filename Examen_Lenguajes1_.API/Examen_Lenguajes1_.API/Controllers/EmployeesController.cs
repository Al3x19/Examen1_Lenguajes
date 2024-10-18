using Examen_Lenguajes1_.API.Constants;
using Examen_Lenguajes1_.API.Database.Entities;
using Examen_Lenguajes1_.API.Dtos.Employees;
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen_Lenguajes1_.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeesController :ControllerBase
    {
        private readonly IEmployeeService _employeesService;

        public EmployeesController(IEmployeeService employeesService)
        {
            this._employeesService = employeesService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<List<EmployeeDto>>>> GetAll(
            string searchTerm = "", 
            int page = 1) 
        {
            var response = await _employeesService.GetEmployeesListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<EmployeeDto>>> Get(Guid id) 
        {
            var response = await _employeesService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<EmployeeDto>>> Create(EmployeeCreateDto dto) 
        {
            var response = await _employeesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<EmployeeDto>>> Edit(EmployeeEditDto dto, Guid id) 
        {
            var response = await _employeesService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HR}")]
        public async Task<ActionResult<ResponseDto<EmployeeDto>>> Delete(Guid id) 
        {
            var response = await _employeesService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
