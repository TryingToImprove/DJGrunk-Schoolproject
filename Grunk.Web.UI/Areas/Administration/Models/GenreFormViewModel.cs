using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class GenreFormViewModel
    {
        public int? GenreId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}