using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class UpdateReviewsViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public string Url { get; set; }
    }
}