using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PayCompute.Entity;
using PayCompute.Services.ViewModels;

namespace PayComputee.Models
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<Employee, EmployeeEditViewModel>()
                .ForMember(dest => dest.ImageUrl, act => act.Ignore());

            CreateMap<EmployeeEditViewModel, Employee>()
                .ForMember(dest => dest.ImageUrl, act => act.Ignore());

            CreateMap<Employee, EmployeeDetailViewModel>();
              


        }
    }
}
