using AutoMapper;
using CourseLibrary.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Author, Models.AuthorDTO>()
                .ForMember(
                dest => dest.Name,
                opt =>
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                dest => dest.Age,
                opt =>
                    opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
            CreateMap<Models.AuthorForCreationDTO, Entities.Author>();

            CreateMap<Entities.Author, Models.AuthorFullDTO>();

        }
    }
}
