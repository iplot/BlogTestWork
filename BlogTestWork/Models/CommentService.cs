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
                var postSet = getDbSet<Post>();

                if (search == "")
                {
                    return postSet.Select(x => new CommentVM
                    {
                        Date = x.Date.ToString(),
                        Text = x.Text,
                        UserName = x.User.UserName
                    }).OrderByDescending(x => x.Date);
                }
                else
                {
                    return postSet
                                .Where(x => x.Text.Contains(search) || x.User.UserName == search)
                                .Select(x => new CommentVM 
                                {
                                    Date = x.Date.ToString(), Text = x.Text, UserName = x.User.UserName
                                })
                                .OrderByDescending(x => x.Date);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewComment(NewCommentVM comment)
        {
            Post postEntity = new Post {Date = comment.Date, Text = comment.Text};

            try
            {
                var userSet = getDbSet<User>();

                if (userSet.Any(x => x.UserName == comment.UserName && x.Gender == comment.Gender))
                {
                    int userId = userSet.Where(x => x.UserName == comment.UserName).Select(x => x.Id).First();
                    postEntity.UserId = userId;

                    getDbSet<Post>().Add(postEntity);
                }
                else
                {
                    User userEntity = new User {Gender = comment.Gender, UserName = comment.UserName};
                    postEntity.User = userEntity;

                    getDbSet<Post>().Add(postEntity);
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