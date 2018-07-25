using SimpleWebApp.Models;
using System.Collections.Generic;

namespace SimpleWebApp.ViewModels
{
    public class HomeViewModel
    {
        public Post Post { get; set; }
        public List<Post> Posts { get; set; }
    }
}
