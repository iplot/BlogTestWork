using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BlogTestWork.Models.DbModels;
using BlogTestWork.Models.ViewModels;

namespace BlogTestWork.Models
{
    public class CommentService
    {
        private readonly DbContext _context;

        public CommentService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<CommentVM> GetComments(string search = "")
        {
            try
            {
                var commentSet = getDbSet<Comment>();

                if (search == "")
                {
                    return commentSet.Select(x => new CommentVM
                    {
                        Date = x.Date.ToString(),
                        Text = x.Text,
                        UserName = x.User.UserName
                    }).OrderBy(x => x.Date);
                }
                else
                {
                    return commentSet
                                .Where(x => x.Text.Contains(search) || x.User.UserName == search)
                                .Select(x => new CommentVM 
                                {
                                    Date = x.Date.ToString(), Text = x.Text, UserName = x.User.UserName
                                })
                                .OrderBy(x => x.Date);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewComment(NewCommentVM comment)
        {
            Comment commentEntity = new Comment {Date = comment.Date, Text = comment.Text};

            try
            {
                var userSet = getDbSet<User>();

                if (userSet.Any(x => x.UserName == comment.UserName && x.Gender == comment.Gender))
                {
                    int userId = userSet.Where(x => x.UserName == comment.UserName).Select(x => x.Id).First();
                    commentEntity.UserId = userId;

                    getDbSet<Comment>().Add(commentEntity);
                }
                else
                {
                    User userEntity = new User {Gender = comment.Gender, UserName = comment.UserName};
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
    }
}