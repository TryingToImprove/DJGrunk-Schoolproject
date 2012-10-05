using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class UpdateProfilesViewModel
    {
        public int ProfileId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}