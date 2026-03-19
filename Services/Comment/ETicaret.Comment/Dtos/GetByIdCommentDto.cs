namespace ETicaret.Comment.Dtos
{
    public class GetByIdCommentDto
    {
        public int UserCommentId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string CommentMessage { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
    }
}

