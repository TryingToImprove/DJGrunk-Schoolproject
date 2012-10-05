using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using Grunk.Web.Controllers.Admin;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    public class TextController : BaseController
    {
        IStaticTextService StaticTextService;
        public TextController(IStaticTextService staticTextService)
        {
            this.StaticTextService = staticTextService;
        }

        //
        // GET: /Administration/Text/?position=Frontpage

        public ActionResult Update(string position)
        {
            var staticText = StaticTextService.GetByPosition(position);

            return View(new UpdateStaticText
            {
                Position = position,
                Header = staticText.Header,
                Text = staticText.Text
            });
        }

        //
        // POST: /Administration/Text/?position=Frontpage
        [HttpPost]
        public ActionResult Update(string position, UpdateStaticText requestedViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StaticTextService.Update(position, requestedViewModel.Header, requestedViewModel.Text);

                    Messages.Add("Gemt", "Indholdet er blevet gemt", Services.MessageType.Success);
                }
            }
            catch
            {
                Messages.AddStandardUpdateError();
            }

            requestedViewModel.Position = position;

            return View(requestedViewModel);
        }

    }
}
