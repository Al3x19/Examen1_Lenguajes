using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Examen_Lenguajes1_.API.Database.Entities
{
    [Table("Requests", Schema = "dbo")]
    public class RequestEntity : BaseEntity
    {
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(30)]
        [Column("type")]
        public string Type { get; set; }

        [Display(Name = "Razón")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(150)]
        [Column("reason")]
        public string Reason { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [Column("state")]
        [StringLength(10)]
        public string State {  get; set; }

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [Column("submit_date")]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "Fecha de finalizacion")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        public virtual EmployeeEntity CreatedByUser { get; set; }
        public virtual EmployeeEntity UpdatedByUser { get; set; }
    }
}
