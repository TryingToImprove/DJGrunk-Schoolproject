using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Web.Models;
using Grunk.Domain;

namespace Grunk.Web.UI.Models
{
    public class IndexContactViewModel : BaseViewModel
    {
        public StaticText Text { get; set; }
        public SendMailFormViewModel MailForm { get; set; }
        public StoreDetails StoreDetails { get; set; }
    }
}