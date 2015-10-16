using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTestWork.Infrastructure
{
    public class FutureDateRestriction : ValidationAttribute
    {
        public FutureDateRestriction()
        {
            ErrorMessage = "You can't post from future. Sorry";
        }

        public override bool IsValid(object value)
        {
            DateTime dateVal = (DateTime) value;

            bool test = dateVal <= DateTime.Now;
            return dateVal <= DateTime.Now;
        }
    }
}