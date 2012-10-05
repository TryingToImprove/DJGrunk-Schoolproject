using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class Picture
    {
        public override string ToString()
        {
            return this.Path + "/" + this.FileName;
        }
    }
}
