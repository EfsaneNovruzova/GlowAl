using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.FavoriteDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using System.Net;
using System.Linq.Expressions;
using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Persistence.Repositories;

namespace GlowAl.Persistence.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IRepository<CareProduct> _productRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository, IRepository<CareProduct> productRepository)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<FavoriteGetDto>> AddFavoriteAsync(string userId, FavoriteCreateDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
                return new BaseResponse<FavoriteGetDto>("Product not found", HttpStatusCode.NotFound);

            var exists = _favoriteRepository
                .GetByFiltered(f => f.UserId == userId && f.ProductId == dto.ProductId)
                .Any();

            if (exists)
                return new BaseResponse<FavoriteGetDto>("Product already in favorites", HttpStatusCode.BadRequest);

            var favorite = new Favorite
            {
                UserId = userId,
                ProductId = dto.ProductId,
                CreatedAt = DateTime.UtcNow
            };

            await _favoriteRepository.AddAsync(favorite);
            await _favoriteRepository.SaveChangeAsync();

            var result = new FavoriteGetDto
            {
                Id = favorite.Id,
                ProductId = favorite.ProductId,
                ProductName = product.Name,
                UserId = favorite.UserId,
                CreatedAt = favorite.CreatedAt
            };

            return new BaseResponse<FavoriteGetDto>("Favorite added successfully", result, HttpStatusCode.Created);
        }

        public async Task<BaseResponse<bool>> RemoveFavoriteAsync(string userId, Guid productId)
        {
            var favorite = _favoriteRepository
                .GetByFiltered(f => f.UserId == userId && f.ProductId == productId)
                .FirstOrDefault();

            if (favorite == null)
                return new BaseResponse<bool>("Favorite not found", HttpStatusCode.NotFound);

            _favoriteRepository.Delete(favorite);
            await _favoriteRepository.SaveChangeAsync();

            return new BaseResponse<bool>("Favorite removed successfully", true, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<List<FavoriteGetDto>>> GetMyFavoritesAsync(string userId)
        {
            var favorites = _favoriteRepository
                .GetByFiltered(f => f.UserId == userId, new Expression<Func<Favorite, object>>[] { f => f.Product })
                .ToList();

            var result = favorites.Select(f => new FavoriteGetDto
            {
                Id = f.Id,
                ProductId = f.ProductId,
                ProductName = f.Product.Name,
                UserId = f.UserId,
                CreatedAt = f.CreatedAt
            }).ToList();

            return new BaseResponse<List<FavoriteGetDto>>("Success", result, HttpStatusCode.OK);
        }
    }
}


