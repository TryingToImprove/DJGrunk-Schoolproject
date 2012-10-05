using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grunk.Web.Models;
using Grunk.Domain;

namespace Grunk.Web.UI.Models
{
    public class VerifyPurchaseShopViewModel : BaseViewModel
    {
        public string Verify { get; set; }
        public Album Album { get; set; }
    }
}