using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfDeploySample.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        [ForeignKey("BlogPostId")]
        public BlogPost ParentPost { get; set; }
        public string Comment { get; set; }
    }
}