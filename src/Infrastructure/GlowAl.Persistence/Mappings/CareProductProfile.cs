using AutoMapper;
using GlowAl.Domain.Entities;
using GlowAl.Application.DTOs.CareProductDtos;
using System.Collections.Generic;
using System.Linq;

public class CareProductProfile : Profile
{
    public CareProductProfile()
    {
        // Create mapping: CareProductCreateDto → CareProduct
        CreateMap<CareProductCreateDto, CareProduct>()
            .ForMember(dest => dest.ProductProblems,
                opt => opt.MapFrom(src =>
                    (src.SkinProblemIds ?? new List<Guid>())
                        .Select(id => new ProductProblem { SkinProblemId = id })
                        .ToList()));

        // Update mapping: CareProductUpdateDto → CareProduct
        CreateMap<CareProductUpdateDto, CareProduct>()
            .ForMember(dest => dest.ProductProblems,
                opt => opt.MapFrom(src =>
                    (src.SkinProblemIds ?? new List<Guid>())
                        .Select(id => new ProductProblem { SkinProblemId = id })
                        .ToList()));

        // Entity → DTO mapping: CareProduct → CareProductGetDto
        CreateMap<CareProduct, CareProductGetDto>()
            .ForMember(dest => dest.SkinProblemIds,
                opt => opt.MapFrom(src =>
                    src.ProductProblems != null
                        ? src.ProductProblems.Select(pp => pp.SkinProblemId).ToList()
                        : new List<Guid>()))
            .ForMember(dest => dest.SkinProblemNames,
                opt => opt.MapFrom(src =>
                    src.ProductProblems != null
                        ? src.ProductProblems.Select(pp => pp.SkinProblem.Name).ToList()
                        : new List<string>()));
    }
}
