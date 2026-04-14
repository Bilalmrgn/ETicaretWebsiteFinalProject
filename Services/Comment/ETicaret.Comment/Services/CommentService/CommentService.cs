using ETicaret.Comment.Context;
using ETicaret.Comment.Dtos;
using ETicaret.Comment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETicaret.Comment.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly CommentDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(CommentDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            //gelen token'dan kullanıcının id sini oku
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (userId == null)
            {
                throw new Exception("kullanıcı bulunamadı (CommentService createCommentAsync metodu)");
            }

            var newComment = new UserComment
            {
                Status = true,
                Rating = createCommentDto.Rating,
                CreatedDate = DateTime.Now,
                CommentMessage = createCommentDto.CommentMessage,
                Email = createCommentDto.Email,
                NameSurname = createCommentDto.NameSurname,
                ProductId = createCommentDto.ProductId,
                UserId = userId
            };

            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var comment = await _context.UserComments.FindAsync(commentId);

            if (comment == null)
            {
                throw new Exception("Comment Bulunamadi (CommentService/DeleteCommentAsync)");
            }

            _context.UserComments.Remove(comment);

            await _context.SaveChangesAsync();
        }

        public async Task<GetByIdCommentDto> GetCommentById(int commentId)
        {
            //gelen token'dan kullanıcının id sini oku
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var comment = await _context.UserComments.FindAsync(commentId);

            return new GetByIdCommentDto
            {
                UserCommentId = comment.UserCommentId,
                CommentMessage = comment.CommentMessage,
                Rating = comment.Rating,
                CreatedDate = comment.CreatedDate,
                Status = comment.Status,
                ProductId = comment.ProductId,
                NameSurname = comment.NameSurname,
                Email = comment.Email,
                UserId = userId
            };
        }

        public async Task<List<UserComment>> GetCommentListAsync()
        {
            return await _context.UserComments.ToListAsync();
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            //gelen token'dan kullanıcının id sini oku
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var existingComment = await _context.UserComments.FindAsync(updateCommentDto.UserCommentId);

            if (existingComment == null)
            {
                throw new Exception("Yorum bulunamadı.");
            }

            // 2. Sadece değişecek alanları veritabanından gelen nesneye ata
            existingComment.CommentMessage = updateCommentDto.CommentMessage;
            existingComment.Rating = updateCommentDto.Rating;
            existingComment.CreatedDate = DateTime.Now;
            existingComment.Email = updateCommentDto.Email;
            existingComment.NameSurname = updateCommentDto.NameSurname;

            await _context.SaveChangesAsync();
        }

        //product a ait bütün yorumları listeleme
        public async Task<List<ResultCommentListDto>> GetAllCommentsByProductIdAsync(string productId)
        {
            return await _context.UserComments
            .Where(x => x.ProductId == productId)
              .Select(y => new ResultCommentListDto
              {
                  UserCommentId = y.UserCommentId,
                  CommentMessage = y.CommentMessage,
                  Rating = y.Rating,
                  CreatedDate = y.CreatedDate,
                  Status = y.Status,
                  ProductId = y.ProductId,
                  NameSurname = y.NameSurname,
                  Email = y.Email
                  // Eğer DTO'nda UserId de varsa onu da ekleyebilirsin:
                  // UserId = y.UserId 
              }).ToListAsync();
        }
    }
}
