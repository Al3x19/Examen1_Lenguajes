using System.ComponentModel.DataAnnotations;

namespace Examen_Lenguajes1_.API.Dtos.Employees
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

       public string Password { get; set; }

        public string UserName { get; set; }

        public string Position { get; set; }

        public DateTime StartDate { get; set; }

    }
}
