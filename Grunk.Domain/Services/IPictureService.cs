using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Services
{
    public interface IPictureService
    {
        void ResizeAndSave(string path, string filename, int width, int height, out string nPath, out string nFileName);

        void Delete(string path, string fileName);
    }
}
