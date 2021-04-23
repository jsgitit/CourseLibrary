using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course, Models.CourseDTO>();
            CreateMap<Models.CourseForCreationDTO, Entities.Course>();
            CreateMap<Models.CourseForUpdateDTO, Entities.Course>();
            CreateMap<Entities.Course, Models.CourseForUpdateDTO>();
        }
    }
}
