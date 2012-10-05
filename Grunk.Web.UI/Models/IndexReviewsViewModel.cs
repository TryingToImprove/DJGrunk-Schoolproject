using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;
using Grunk.Web.Models;

namespace Grunk.Web.UI.Models
{
    public class IndexReviewsViewModel : BaseViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }

        public PagedViewModel Paged { get; set; }
    }
}