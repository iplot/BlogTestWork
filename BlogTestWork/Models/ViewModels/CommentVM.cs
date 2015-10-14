using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTestWork.Models.ViewModels
{
    public class CommentVM
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}