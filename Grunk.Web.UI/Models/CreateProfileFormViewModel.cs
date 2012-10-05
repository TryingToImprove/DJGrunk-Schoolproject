using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;

namespace Grunk.Web.UI.Models
{
    public class CreateProfileFormViewModel
    {
        [Required(ErrorMessage="*")]
        [DisplayName("Brugernavn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Kodeord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Beskrivelse")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Profilbillede")]
        public HttpPostedFileBase Picture { get; set; }
    }
}
