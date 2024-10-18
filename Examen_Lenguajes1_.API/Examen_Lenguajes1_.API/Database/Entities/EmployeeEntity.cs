using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen_Lenguajes1_.API.Database.Entities
{
    public class EmployeeEntity : IdentityUser
    {

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(30)]
        [Column("position")]
        public string Position { get; set; }

        [Display(Name = "Fecha de entrada")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

    }
}
