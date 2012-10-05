using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class CreateShopViewModel
    {
        public ProductFormViewModel ProductForm { get; set; }
        [Required]
        public HttpPostedFileBase Cover { get; set; }
    }
}