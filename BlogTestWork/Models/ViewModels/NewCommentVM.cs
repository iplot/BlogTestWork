using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BlogTestWork.Infrastructure;
using BlogTestWork.Models.DbModels;

namespace BlogTestWork.Models.ViewModels
{
    public class NewCommentVM
    {
        [Required]
        [RegularExpression("[A-Za-z0-9]+")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [FutureDateRestriction]
        public DateTime UserDate { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string Text { get; set; }

        public int? LastId { get; set; }
    }
}