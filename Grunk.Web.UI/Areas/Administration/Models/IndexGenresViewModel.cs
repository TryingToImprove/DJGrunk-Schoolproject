using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class IndexGenresViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int? GenreId{ get; set; }
    }
}