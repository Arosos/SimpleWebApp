using SimpleWebApp.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string AuthorID { get; set; }
        public SimpleWebAppUser Author { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Timestamp { get; set; }
        [Required]
        [StringLength(250)]
        public string Content { get; set; }
    }
}
