using Api.DTO;
using AutoMapper;
using Data.Models;

namespace AplicativoNota.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, AutenticaoRequest>().ReverseMap();
            CreateMap<User, LoginRequest>().ReverseMap();
        }
    }
}
