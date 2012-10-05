using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Grunk.Web.UI.Areas.Administration.Models
{
    public class ProductFormViewModel
    {
        [Required]
        [Display(Name = "Album")]
        public string AlbumName { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public string ArtistName { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public Int16 Price { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }


        #region Dropdowns
        public IEnumerable<SelectListItem> PossiblePrices
        {
            get
            {
                return new SelectListItem[] { 
                    new SelectListItem{ Text = 50.ToString(), Value = 50.ToString() },
                    new SelectListItem{ Text = 100.ToString(), Value = 100.ToString() },
                    new SelectListItem{ Text = 150.ToString(), Value = 150.ToString() }
                };
            }
        }
        public IEnumerable<SelectListItem> PossibleGenres { get; set; }
        #endregion
    }
}