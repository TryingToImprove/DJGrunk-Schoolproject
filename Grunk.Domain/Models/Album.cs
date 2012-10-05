using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class Album
    {
        public Picture GetPicture(int width, int height)
        {
            return this.Pictures.OrderByDescending(x => x.UploadedDateTime).First(x => x.Width == width && x.Height == height);
        }
    }
}
