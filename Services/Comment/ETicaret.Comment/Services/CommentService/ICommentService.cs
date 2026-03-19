using ETicaret.Comment.Dtos;
using ETicaret.Comment.Entities;

namespace ETicaret.Comment.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<UserComment>> GetCommentListAsync();
        Task<GetByIdCommentDto> GetCommentById(int commentId);
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task DeleteCommentAsync(int commentId);
        Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<List<ResultCommentListDto>> GetAllCommentsByProductIdAsync(string productId);
    }
}
