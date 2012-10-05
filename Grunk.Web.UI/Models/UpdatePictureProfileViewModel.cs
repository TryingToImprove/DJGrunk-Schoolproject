using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Grunk.Web.UI.Models
{
    public class UpdatePictureProfileViewModel
    {
        [Required(ErrorMessage = "*")]
        [DisplayName("Profilbillede")]
        public HttpPostedFileBase Picture { get; set; }
    }
}