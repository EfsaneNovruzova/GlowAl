using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.SkinTypeDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using System.Net;

namespace GlowAl.Persistence.Services
{
    public class SkinTypeService : ISkinTypeService
    {
        private readonly ISkinTypeRepository _skinTypeRepository;

        public SkinTypeService(ISkinTypeRepository skinTypeRepository)
        {
            _skinTypeRepository = skinTypeRepository;
        }

        public async Task<BaseResponse<SkinTypeGetDto>> AddAsync(SkinTypeCreateDto dto)
        {
            var entity = new SkinType
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _skinTypeRepository.AddAsync(entity);
            await _skinTypeRepository.SaveChangeAsync();

            var result = new SkinTypeGetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return new BaseResponse<SkinTypeGetDto>("Skin type successfully created", result, HttpStatusCode.Created);
        }

        public async Task<BaseResponse<SkinTypeGetDto>> UpdateAsync(SkinTypeUpdateDto dto)
        {
            var entity = await _skinTypeRepository.GetByIdAsync(dto.Id);
            if (entity == null)
                return new BaseResponse<SkinTypeGetDto>("Skin type not found", HttpStatusCode.NotFound);

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            _skinTypeRepository.Update(entity);
            await _skinTypeRepository.SaveChangeAsync();

            var result = new SkinTypeGetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return new BaseResponse<SkinTypeGetDto>("Skin type successfully updated", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<string>> DeleteAsync(Guid id)
        {
            var entity = await _skinTypeRepository.GetByIdAsync(id);
            if (entity == null)
                return new BaseResponse<string>("Skin type not found", HttpStatusCode.NotFound);

            _skinTypeRepository.Delete(entity);
            await _skinTypeRepository.SaveChangeAsync();

            return new BaseResponse<string>("Skin type successfully deleted", HttpStatusCode.OK);
        }

        public async Task<BaseResponse<SkinTypeGetDto>> GetByIdAsync(Guid id)
        {
            var entity = await _skinTypeRepository.GetByIdAsync(id);
            if (entity == null)
                return new BaseResponse<SkinTypeGetDto>("Skin type not found", HttpStatusCode.NotFound);

            var result = new SkinTypeGetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return new BaseResponse<SkinTypeGetDto>("Success", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<SkinTypeGetDto>> GetByNameAsync(string name)
        {
            var entity = _skinTypeRepository.GetByFiltered(e => e.Name == name).FirstOrDefault();
            if (entity == null)
                return new BaseResponse<SkinTypeGetDto>("Skin type not found", HttpStatusCode.NotFound);

            var result = new SkinTypeGetDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return new BaseResponse<SkinTypeGetDto>("Success", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<List<SkinTypeGetDto>>> GetAllAsync()
        {
            var entities = _skinTypeRepository.GetAll().ToList();
            if (!entities.Any())
                return new BaseResponse<List<SkinTypeGetDto>>("No skin types found", HttpStatusCode.NotFound);

            var result = entities.Select(e => new SkinTypeGetDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList();

            return new BaseResponse<List<SkinTypeGetDto>>("Success", result, HttpStatusCode.OK);
        }
    }
}

