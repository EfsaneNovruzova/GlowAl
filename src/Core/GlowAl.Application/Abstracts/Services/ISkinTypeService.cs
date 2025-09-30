using GlowAl.Application.DTOs.SkinTypeDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlowAl.Application.Abstracts.Services
{
    public interface ISkinTypeService
    {
        Task<BaseResponse<SkinTypeGetDto>> AddAsync(SkinTypeCreateDto dto);
        Task<BaseResponse<SkinTypeGetDto>> UpdateAsync(SkinTypeUpdateDto dto);
        Task<BaseResponse<string>> DeleteAsync(Guid id);
        Task<BaseResponse<SkinTypeGetDto>> GetByIdAsync(Guid id);
        Task<BaseResponse<SkinTypeGetDto>> GetByNameAsync(string name);
        Task<BaseResponse<List<SkinTypeGetDto>>> GetAllAsync();
    }
}

