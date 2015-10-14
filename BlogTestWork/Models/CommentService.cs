using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BlogTestWork.Models.DbModels;
using BlogTestWork.Models.ViewModels;

namespace BlogTestWork.Models
{
    public class CommentService : ICommentService
    {
        private readonly DbContext _context;

        public CommentService()
        {
            _context = new BlogContext();
        }

        public IEnumerable<CommentVM> SearchComments(string search)
        {
            try
            {
                var commentSet = getDbSet<Comment>();

                return commentSet
                        .Where(x => x.Text.Contains(search) || x.User.UserName == search)
                        .Select(x => new CommentVM 
                        {
                            Date = x.Date.ToString(), Text = x.Text, UserName = x.User.UserName
                        })
                        .OrderByDescending(x => x.Date);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecentCommentsVM GetRecentComments(DateTime? lastDate)
        {
            try
            {
                var commentSet = getDbSet<Comment>();

                var comments = commentSet.OrderByDescending(x => x.Date);

                var date = comments.Any() ? comments.Select(x => x.Date).First() : lastDate;

                if (lastDate == null)
                {
                    return new RecentCommentsVM
                    {
                        Comments = comments.Select(x =>
                            new CommentVM {Date = x.Date.ToString(), Text = x.Text, UserName = x.User.UserName})
                            .ToList(),
                        LastDateTime = date
                    };
                }
                else
                {
                    return new RecentCommentsVM
                    {
                        Comments = comments.Where(x => x.Date > lastDate)
                            .Select(
                                x => new CommentVM {Date = x.Date.ToString(), Text = x.Text, UserName = x.User.UserName})
                            .ToList(),
                        LastDateTime = date
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewComment(NewCommentVM comment)
        {
            Comment commentEntity = new Comment {Date = DateTime.Now, Text = comment.Text};

            try
            {
                var userSet = getDbSet<User>();

                if (userSet.Any(x => x.UserName == comment.UserName && x.Gender == comment.Gender && x.Date == comment.UserDate))
                {
                    int userId = userSet.Where(x => x.UserName == comment.UserName).Select(x => x.Id).First();
                    commentEntity.UserId = userId;

                    getDbSet<Comment>().Add(commentEntity);
                }
                else
                {
                    User userEntity = new User {Gender = comment.Gender, UserName = comment.UserName, Date = comment.UserDate};
                    commentEntity.User = userEntity;

                    getDbSet<Comment>().Add(commentEntity);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbSet<T> getDbSet<T> () where T : class
        {
            return _context.Set<T>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}