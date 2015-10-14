using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTestWork.Models.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Index("UniqueName", IsUnique = true)]
        [MaxLength(100)]
        [Required]
        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}