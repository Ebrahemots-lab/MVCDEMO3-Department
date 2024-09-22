using ApplicationDAL.Models;
using ApplicationPL.Models;
using AutoMapper;

namespace ApplicationPL.Controllers.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        //Not Impelemented Yet
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }

    }
}
