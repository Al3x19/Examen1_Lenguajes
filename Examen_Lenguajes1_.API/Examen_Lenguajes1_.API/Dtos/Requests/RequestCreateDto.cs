using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_Lenguajes1_.API.Dtos.Requests
{
    public class RequestCreateDto
    {
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(30)]
        public string Type { get; set; }

        [Display(Name = "Razón")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
        [StringLength(150)]
        public string Reason { get; set; }



        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]
     
        public DateTime SubmitDate { get; set; }

        [Display(Name = "Fecha de finalizacion")]
        [Required(ErrorMessage = "El {0} de la solicitud es requerido.")]

        public DateTime EndDate { get; set; }
    }
}
