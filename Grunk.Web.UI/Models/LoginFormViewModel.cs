using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Models
{
    public class LoginFormViewModel
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}