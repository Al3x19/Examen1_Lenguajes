
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Dtos.Employees;

namespace Examen_Lenguajes1_.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseDto<EmployeeDto>> CreateAsync(EmployeeCreateDto dto);
        Task<ResponseDto<EmployeeDto>> DeleteAsync(Guid id);
        Task<ResponseDto<EmployeeDto>> EditAsync(EmployeeEditDto dto, Guid id);
        Task<ResponseDto<EmployeeDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<EmployeeDto>>>> GetEmployeesListAsync(string searchTerm = "", int page = 1);
    }
}