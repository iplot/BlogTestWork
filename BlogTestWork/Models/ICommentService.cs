using System;
using System.Collections.Generic;
using BlogTestWork.Models.ViewModels;

namespace BlogTestWork.Models
{
    public interface ICommentService : IDisposable
    {
        IEnumerable<CommentVM> SearchComments(string search);
//        RecentCommentsVM GetRecentComments(DateTime? lastDate);
        RecentCommentsVM GetRecentComments(int? lastId);
        void AddNewComment(NewCommentVM comment);
    }
}