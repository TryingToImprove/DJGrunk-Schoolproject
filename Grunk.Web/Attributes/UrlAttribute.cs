using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Grunk.Web.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class UrlAttribute : ValidationAttribute
    {
        public bool Optional { get; set; }

        public UrlAttribute(bool optional = false)
        {
            this.Optional = optional;
        }

        public override bool IsValid(object value)
        {
            if (Optional && (value == null))
            {
                return true;
            }
            else if (!Optional && (value == null))
            {
                return false;
            }

            Uri uri;

            bool result = Uri.TryCreate(value.ToString(), UriKind.Absolute, out uri);
            return result;
        }
    }
}
