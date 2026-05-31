using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;

namespace ICICIBANK_HOMELOANS_Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Define your mappings here
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<OrdersDto, Orders>();
            CreateMap<Orders, OrdersDto>();
        }
    }
}
