using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using System.Drawing;
using System.IO;
using System.Web;
using Grunk.Domain.Services;

namespace Grunk.Services
{
    public class PictureService : IPictureService
    {
        private readonly string UploadPath = "/Content/Uploads/";

        public PictureService(string folder)
        {
            this.UploadPath += folder + "/";
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void ResizeAndSave(string path, string filename, int width, int height, out string nPath, out string nFileName)
        {
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(path + "/" + filename)))
            {
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        graphics.DrawImage(img, new Rectangle(0, 0, width, height));
                    }

                    string resizedPath = UploadPath + width + "x" + height;


                    CreateDirectory(HttpContext.Current.Server.MapPath("~" + resizedPath));

                    bitmap.Save(HttpContext.Current.Server.MapPath("~" + resizedPath + "/" + filename));

                    nPath = resizedPath;
                    nFileName = filename;
                }
            }
        }


        public void Delete(string path, string fileName)
        {
            File.Delete(HttpContext.Current.Server.MapPath("~" + path + "/" + fileName));
        }
    }

}
