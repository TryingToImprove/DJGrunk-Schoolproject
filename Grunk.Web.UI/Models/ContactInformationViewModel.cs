using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Domain;

namespace Grunk.Web.UI.Models
{
    public class ContactInformationViewModel
    {
        public string Road { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public string Telephone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public StoreDetails StoreDetails { get; set; }
    }
}