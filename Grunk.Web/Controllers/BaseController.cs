using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Grunk.Security;
using Grunk.Web.Models;

namespace Grunk.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
        }

        public new virtual ICustomPrincipal User
        {
            get { return base.User as ICustomPrincipal; }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            if (model != null && model.GetType().BaseType == typeof(BaseViewModel))
            {
                ((BaseViewModel)filterContext.Controller.ViewData.Model).ProfileData = this.User;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
