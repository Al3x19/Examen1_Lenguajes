using System.ComponentModel.DataAnnotations;

namespace Examen_Lenguajes1_.API.Dtos.Requests
{
    public class RequestDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }


        public string Reason { get; set; }


        public string State { get; set; }

        public DateTime SubmitDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
