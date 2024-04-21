using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Min 3 character")]
        [MaxLength(30,ErrorMessage ="Max 30 character")]
        public string Title { get; set; }=string.Empty;
        [Required]
        [MinLength(3,ErrorMessage ="Min 3 character")]
        [MaxLength(1000,ErrorMessage ="Max 1000 character")]
        public string Content { get; set; }=string.Empty;
    }
}