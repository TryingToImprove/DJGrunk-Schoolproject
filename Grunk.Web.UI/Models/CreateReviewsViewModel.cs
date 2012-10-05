using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Web.Models;
using System.ComponentModel.DataAnnotations;
using Grunk.Web.Attributes;

namespace Grunk.Web.UI.Models
{
    public class CreateReviewsViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase Picture { get; set; }

        [Url(Optional = true)]
        public string Url { get; set; }
    }
}