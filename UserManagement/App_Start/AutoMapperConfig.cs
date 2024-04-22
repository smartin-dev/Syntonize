using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.App_Start
{
    using AutoMapper;
    using UserManagement.Data.Models;
    using UserManagement.DTOs;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            // Add more mappings as needed
        }
    }

    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }
    }

}