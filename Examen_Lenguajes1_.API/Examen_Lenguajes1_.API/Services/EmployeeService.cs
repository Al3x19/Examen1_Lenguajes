using AutoMapper;
using Examen_Lenguajes1_.API.Database.Entities;
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Examen_Lenguajes1_.API.Constants;
using Examen_Lenguajes1_.API.Dtos.Employees;

namespace Examen_Lenguajes1_.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<EmployeeEntity> _userManager;
        private readonly int PAGE_SIZE;

        EmployeeService(
            IMapper mapper,
            IConfiguration configuration,
            UserManager<EmployeeEntity> userManager

            )
        {
            this._mapper = mapper;
            this._configuration = configuration;
            this._userManager = userManager;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<EmployeeDto>>>> GetEmployeesListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var employeesEntityQuery = _userManager.Users
                .Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()) || x.Position.ToLower().Contains(searchTerm.ToLower()));

            int totalEmployees = await employeesEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalEmployees / PAGE_SIZE);

            var employeesEntity = await employeesEntityQuery
                .OrderBy(u => u.UserName)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var employeesDtos = _mapper.Map<List<EmployeeDto>>(employeesEntity);

            return new ResponseDto<PaginationDto<List<EmployeeDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Se encontro el listado...",
                Data = new PaginationDto<List<EmployeeDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalEmployees,
                    TotalPages = totalPages,
                    Items = employeesDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<EmployeeDto>> GetByIdAsync(Guid id)
        {
            var employeeEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

            if (employeeEntity is null)
            {
                return new ResponseDto<EmployeeDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro registro..."
                };
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return new ResponseDto<EmployeeDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "registro encontrado",
                Data = employeeDto,
            };
        }

        public async Task<ResponseDto<EmployeeDto>> CreateAsync(EmployeeCreateDto dto)
        {

            var employeeEntity = new EmployeeEntity
            {
                Email = dto.Email,
                UserName = dto.UserName,
                Position = dto.Position,
                StartDate = DateTime.Now,
            };
            await _userManager.CreateAsync(employeeEntity, dto.Password);
            await _userManager.AddToRoleAsync(employeeEntity, RolesConstant.USER);

            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return new ResponseDto<EmployeeDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Se creo correctamente...",
                Data = employeeDto
            };
        }

        public async Task<ResponseDto<EmployeeDto>> DeleteAsync(Guid id)
        {
            var employeeEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

            if (employeeEntity is null)
            {
                return new ResponseDto<EmployeeDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro registro..."
                };
            }

            await _userManager.DeleteAsync(employeeEntity);


            return new ResponseDto<EmployeeDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Se elimino satisfactoriamente..."
            };

        }

        public async Task<ResponseDto<EmployeeDto>> EditAsync(EmployeeEditDto dto, Guid id)
        {
            var employeeEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

            if (employeeEntity is null)
            {
                return new ResponseDto<EmployeeDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontro registro..."
                };
            }
            _mapper.Map<EmployeeEditDto, EmployeeEntity>(dto, employeeEntity);

            employeeEntity.Email = dto.Email;
            employeeEntity.UserName = dto.UserName;
            employeeEntity.Position = dto.Position;
            employeeEntity.StartDate = dto.StartDate;

            await _userManager.UpdateAsync(employeeEntity);


            var categoryDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return new ResponseDto<EmployeeDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Se edito exitosamente",
                Data = categoryDto
            };
        }



    }
}
