using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreatedBrandDto, Brand>().ReverseMap();
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
        }
    }
}
