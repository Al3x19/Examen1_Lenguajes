using AutoMapper;
using Examen_Lenguajes1_.API.Dtos.Requests;
using Examen_Lenguajes1_.API.Database.Entities;



namespace Examen_Lenguajes1_.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            MapsForRequests();
        }


        private void MapsForRequests()
        {
            CreateMap<RequestEntity, RequestDto>();
            CreateMap<RequestCreateDto, RequestEntity>();
            CreateMap<RequestEditDto, RequestEntity>();
        }
    }
}
