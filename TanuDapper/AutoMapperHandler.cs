using AutoMapper;
using TanuDapper.Automapper;
using TanuDapper.Model;

namespace TanuDapper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<EmployeeModel, Employee>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.PersonalDetails, opt => opt.MapFrom(src => src.Gender + " " + src.Email))
                .ReverseMap();
        }
    }
}
