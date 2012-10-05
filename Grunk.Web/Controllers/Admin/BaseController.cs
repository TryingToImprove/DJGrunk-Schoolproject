using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Grunk.Security;
using Grunk.Web.Services;

namespace Grunk.Web.Controllers.Admin
{
    public abstract class BaseController : Controller
    {
        string tempFieldName = "Messages";

        public new virtual ICustomPrincipal User
        {
            get { return base.User as ICustomPrincipal; }
        }

        public MessageHandler Messages
        {
            get
            {
                if (TempData[tempFieldName] == null)
                {
                    TempData["Messages"] = MessageHandler.CreateNew();
                }

                return (MessageHandler)TempData[tempFieldName];
            }
            set
            {
                TempData["Messages"] = value;
            }
        }
    }
}
