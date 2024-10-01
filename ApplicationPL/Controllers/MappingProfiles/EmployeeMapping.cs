using ApplicationDAL.Models;
using ApplicationPL.Models;
using AutoMapper;

namespace ApplicationPL.Controllers.MappingProfiles
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping() 
        {
            //Create Source and Distination 
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
            CreateMap<ApplicationUser, UsersViewModel>().ReverseMap();
        }
    }
}
