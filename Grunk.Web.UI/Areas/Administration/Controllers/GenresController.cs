using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grunk.Web.Controllers.Admin;
using Grunk.Domain.Services;
using Grunk.Web.UI.Areas.Administration.Models;
using System.Text;

namespace Grunk.Web.UI.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : BaseController
    {
        IProductService ProductService;
        public GenresController(IProductService productService)
        {
            ProductService = productService;
        }

        //
        // GET: /Administration/Genres/

        public ActionResult Index(int? id)
        {
            return View(new IndexGenresViewModel
            {
                Genres = ProductService.GetGenres(),
                GenreId = id
            });
        }


        //
        // GET: /Administration/Genres/Delete/1

        public ActionResult Delete(int id)
        {
            try
            {
                var genre = ProductService.GetGenreById(id);

                if (genre.Albums.Any())
                {
                    StringBuilder strBuilder = new StringBuilder("Det er ikke muligt at slette genre hvor der er albums til afhænger af den. Disse albums er afhængig af " + genre.Name + "<br />");

                    strBuilder.AppendLine("<ul>");
                    foreach (var album in genre.Albums.ToArray())
                    {
                        strBuilder.AppendLine("<li>" + album.Name + " af " + album.Artist.Name + "</li>");
                    }
                    strBuilder.AppendLine("</ul>");

                    Messages.Add("Genren blev ikke slettet", strBuilder.ToString(), Services.MessageType.Error);
                }
                else
                {
                    ProductService.DeleteGenreById(id);

                    Messages.Add("Genren er slettet", "Genren er blevet slettet", Services.MessageType.Success);
                }
            }
            catch
            {
                Messages.AddStandardDeleteError();
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Administration/Genres/Create
        [ChildActionOnly]
        public ActionResult CreateOrUpdate(int? id)
        {
            string name = string.Empty;
            if (id.HasValue)
            {
                try
                {
                    var genre = ProductService.GetGenreById(id.Value);
                    name = genre.Name;
                }
                catch
                {
                    Messages.AddStandardRetriveError();
                }
            }
            return PartialView(new GenreFormViewModel() { GenreId = id, Name = name });
        }

        //
        // POST: /Administration/Genres/Create
        [HttpPost]
        public ActionResult CreateOrUpdate(int? id, GenreFormViewModel requestedViewModel)
        {
            if (ModelState.IsValid)
            {
                if (requestedViewModel.GenreId.HasValue)
                {
                    try
                    {
                        //UPDATE
                        ProductService.UpdateGenre(requestedViewModel.GenreId.Value, requestedViewModel.Name);

                        Messages.Add("Ændret!", "Genren er blevet ændret", Services.MessageType.Success);
                    }
                    catch
                    {
                        Messages.AddStandardUpdateError();

                        return RedirectToAction("Index", new { id = "" });
                    }
                }
                else
                {
                    //CREATE
                    try
                    {
                        ProductService.CreateGenre(requestedViewModel.Name);

                        Messages.Add("Oprettet!", "Genren er blevet oprettet", Services.MessageType.Success);
                    }
                    catch
                    {
                        Messages.AddStandardCreateError();

                        return RedirectToAction("Index", new { id = "" });
                    }

                }
                return RedirectToAction("Index");
            }

            Messages.Add("Der skete en fejl", "Genren er ikke blevet gemt", Services.MessageType.Error);

            return RedirectToAction("Index");

        }

    }
}
