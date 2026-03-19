using ETicaret.Comment.Dtos;
using ETicaret.Comment.Entities;
using ETicaret.Comment.Services.CommentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var comments = await _commentService.GetCommentListAsync();
            return Ok(comments);
        }

        //Get by id comment
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetByIdComment(int commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);

            return Ok(comment);

        }

        //create comment
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            await _commentService.CreateCommentAsync(dto);
            return Ok("Comment başarıyla oluşturuldu");
        }

        //update comment
        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto dto)
        {
            await _commentService.UpdateCommentAsync(dto);
            return Ok("Comment başarıyla güncellendi");
        }

        //delete comment
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok("Comment başarıyla silindi");
        }

        //ProductId sine göre bütün yorumları listeleme
        [HttpGet("GetCommentListByProductId/{productId}")]
        public async Task<IActionResult> GetCommentByProductId(string productId)
        {
            var comments = await _commentService.GetAllCommentsByProductIdAsync(productId);
            return Ok(comments);
        }

    }
}
