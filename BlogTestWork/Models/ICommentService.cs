using System;
using System.Collections.Generic;
using BlogTestWork.Models.ViewModels;

namespace BlogTestWork.Models
{
    public interface ICommentService : IDisposable
    {
        IEnumerable<CommentVM> SearchComments(string search);
        RecentCommentsVM GetRecentComments(DateTime? lastDate);
        void AddNewComment(NewCommentVM comment);
    }
}