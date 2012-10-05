using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Grunk.Web.UI.Models
{
    public class PagedViewModel
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
        public object RouteData { get; set; }
    }
}