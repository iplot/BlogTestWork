using System.Data.Entity;

namespace BlogTestWork.Models.DbModels
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}