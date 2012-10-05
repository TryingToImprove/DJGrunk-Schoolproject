using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class IndexProfilesViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
    }
}