using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class IndexReviewsViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }
    }
}