using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.CommentDto
{
    public class CreateCommentDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string CommentMessage { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string ProductId { get; set; }
    }
}
