using AutoMapper;
using GenericRepositoryUnitOfWork.Core.Dto;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using GenericRepositoryUnitOfWork.Core.Models;
using GenericRepositoryUnitOfWork.DAL.Entities;


namespace GenericRepositoryUnitOfWork.Core.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentVM, Department>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeVM, Employee>();
            CreateMap<Employee, EmployeeList>();
            CreateMap<SignInVM, SignInDto>();
            CreateMap<SignUpVM, SignUpDto>();
            CreateMap<Address, UserAddressDto>();
            CreateMap<UserAddressDto, Address>();
            CreateMap<ApplicationUser, UserDto>();



        }

    }
}
