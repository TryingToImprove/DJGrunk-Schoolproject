using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class UpdateStaticText
    {
        [Required]
        [MaxLength(150)]
        public string Header { get; set; }
        [Required]
        public string Text { get; set; }

        public string Position { get; set; }
    }
}