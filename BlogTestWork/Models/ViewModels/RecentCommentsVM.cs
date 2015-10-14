using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTestWork.Models.ViewModels
{
    public class RecentCommentsVM
    {
        public IEnumerable<CommentVM> Comments { get; set; }
        public DateTime? LastDateTime { get; set; }
    }
}