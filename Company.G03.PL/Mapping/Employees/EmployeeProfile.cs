using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.Models;

namespace Company.G03.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public  EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
