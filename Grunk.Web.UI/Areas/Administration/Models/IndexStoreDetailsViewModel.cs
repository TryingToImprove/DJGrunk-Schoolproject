using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class IndexStoreDetailsViewModel
    {
        public Domain.StoreDetails StoreDetails { get; set; }

        [Required]
        [DisplayName("Navn")]
        public string Name { get; set; }

        public AddressInformation Address { get; set; }
        public ContactInformation Contact { get; set; }

        public class ContactInformation
        {
            [Required]
            [DisplayName("Telefonnummer")]
            public string Telephone { get; set; }
            public string Fax { get; set; }
            [Required]
            [DisplayName("Email")]
            public string Email { get; set; }
        }

        public class AddressInformation
        {
            [Required]
            [DisplayName("Nummer")]
            public int Number { get; set; }
            [Required]
            [DisplayName("Vejnavn")]
            public string Road { get; set; }
            [Required]
            [DisplayName("Postnummer")]
            public int Postal { get; set; }
            [Required]
            [DisplayName("By")]
            public string Town { get; set; }
        }

        public IEnumerable<string> OpeningHour { get; set; }
        public IEnumerable<string> CloseingHour { get; set; }
    }
}