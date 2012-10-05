using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Models
{
    public class UpdateDescriptionProfileViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }
    }
}