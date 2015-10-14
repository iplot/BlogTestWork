using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTestWork.Models.DbModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        public string Text { get; set; }

        [Column("Date", TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}