using AutoMapper;
using ProjectAPI.DTOs;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NationalPark, NationalParkDto>();
            CreateMap<NationalParkDto, NationalPark>();
        }
    }
}
