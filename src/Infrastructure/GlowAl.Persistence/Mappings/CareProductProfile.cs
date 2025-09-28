using AutoMapper;
using GlowAl.Application.DTOs.CareProductDtos;
using GlowAl.Domain.Entities;

public class CareProductProfile : Profile
{
    public CareProductProfile()
    {
        CreateMap<CareProductCreateDto, CareProduct>();
        CreateMap<CareProductUpdateDto, CareProduct>();
        CreateMap<CareProduct, CareProductGetDto>();
    }
}





