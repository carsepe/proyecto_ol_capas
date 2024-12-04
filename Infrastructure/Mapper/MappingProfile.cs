using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using System.Data;
using System;

namespace Infraestructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}
