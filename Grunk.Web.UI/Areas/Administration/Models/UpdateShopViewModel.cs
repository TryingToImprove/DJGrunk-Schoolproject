using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class UpdateShopViewModel
    {
        public int AlbumId { get; set; }
        public ProductFormViewModel ProductForm { get; set; }
        public HttpPostedFileBase Cover { get; set; }
    }
}