using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_Lenguajes1_.API.Dtos.Employees
{
    public class EmployeeCreateDto
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "EL campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es valido.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe ser segura y contener al menos 8 caracteres, incluyendo minúsculas, mayúsculas, números y caracteres especiales.")]
        public string Password { get; set; }


        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(30)]
     
        public string UserName { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(30)]
        public string Position { get; set; }

        [Display(Name = "Fecha de entrada")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        public DateTime StartDate { get; set; }

    }
}
