using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BlogTestWork.Models.DbModels;

namespace BlogTestWork.Models.ViewModels
{
    public class NewCommentVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string Text { get; set; }
    }
}