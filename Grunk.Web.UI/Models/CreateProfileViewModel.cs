using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Web.Models;

namespace Grunk.Web.UI.Models
{
    public class CreateProfileViewModel : BaseViewModel
    {
        public CreateProfileFormViewModel CreateProfileForm { get; set; }

        public LoginFormViewModel LoginForm { get; set; }
    }
}