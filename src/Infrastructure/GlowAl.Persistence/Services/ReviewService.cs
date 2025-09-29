using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.ReviewDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Repositories;
using System.Linq.Expressions;
using System.Net;

namespace GlowAl.Persistence.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRepository<CareProduct> _productRepository;

        public ReviewService(IReviewRepository reviewRepository, IRepository<CareProduct> productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<ReviewGetDto>> CreateAsync(Guid productId, string userId, ReviewCreateDto dto)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return new BaseResponse<ReviewGetDto>("Product not found", HttpStatusCode.NotFound);

            var review = new Review
            {
                ProductId = productId,
                UserId = userId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangeAsync();

            var result = new ReviewGetDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                UserId = review.UserId,
                UserName = review.User?.UserName ?? "Unknown",
                CreatedAt = review.CreatedAt
            };

            return new BaseResponse<ReviewGetDto>("Review created successfully", result, HttpStatusCode.Created);
        }

        public async Task<BaseResponse<List<ReviewGetDto>>> GetByProductIdAsync(Guid productId)
        {
            var reviews = _reviewRepository
                .GetByFiltered(r => r.ProductId == productId, new Expression<Func<Review, object>>[] { r => r.User })
                .ToList();

            var result = reviews.Select(r => new ReviewGetDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserId = r.UserId,
                UserName = r.User?.UserName ?? "Unknown",
                CreatedAt = r.CreatedAt
            }).ToList();

            return new BaseResponse<List<ReviewGetDto>>("Success", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<ReviewGetDto>> UpdateAsync(Guid reviewId, string userId, ReviewUpdateDto dto)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                return new BaseResponse<ReviewGetDto>("Review not found", HttpStatusCode.NotFound);

            if (review.UserId != userId)
                return new BaseResponse<ReviewGetDto>("You are not authorized to update this review", HttpStatusCode.Forbidden);

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            _reviewRepository.Update(review);
            await _reviewRepository.SaveChangeAsync();

            var result = new ReviewGetDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                UserId = review.UserId,
                UserName = review.User?.UserName ?? "Unknown",
                CreatedAt = review.CreatedAt
            };

            return new BaseResponse<ReviewGetDto>("Review updated successfully", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid reviewId, string userId, bool isAdmin)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                return new BaseResponse<bool>("Review not found", HttpStatusCode.NotFound);

            if (review.UserId != userId && !isAdmin)
                return new BaseResponse<bool>("You are not authorized to delete this review", HttpStatusCode.Forbidden);

            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangeAsync();

            return new BaseResponse<bool>("Review deleted successfully", true, HttpStatusCode.OK);
        }
    }
}
