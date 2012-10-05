using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.UI.Models;
using Grunk.Web.Controllers;
using Grunk.Domain.Services;

namespace Grunk.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        IStaticTextService StaticTextService;

        public HomeController(IStaticTextService staticTextService)
        {
            this.StaticTextService = staticTextService;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new IndexHomeViewModel() { 
                Text = StaticTextService.GetByPosition("Frontpage"),
                MenuKey = "home" 
            });
        }

    }
}
