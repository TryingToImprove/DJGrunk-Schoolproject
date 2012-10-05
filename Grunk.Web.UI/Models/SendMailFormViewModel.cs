using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Grunk.Web.UI.Models
{
    public class SendMailFormViewModel
    {
        [Required]
        [DisplayName("Navn")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Telefon nummer")]
        public string Telephone { get; set; }

        [DisplayName("Adresse")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Kommentar")]
        public string Text { get; set; }
    }
}