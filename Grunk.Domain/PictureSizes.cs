using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public static class PictureSizes
    {
        public class Size
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public static List<Size> Profiles = new List<Size>(){
            new Size{ Width = 50, Height = 50 },
            new Size{ Width = 250, Height = 250}
        };

        public static List<Size> Products = new List<Size>(){
            new Size{ Width = 200, Height = 200 },
            new Size{ Width = 50, Height = 50}
        };

        public static List<Size> Reviews = new List<Size>(){
            new Size{ Width = 100, Height = 100 }
        };
    }
}
