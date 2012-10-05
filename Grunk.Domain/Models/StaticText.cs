using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class StaticText
    {

        public string HtmlText
        {
            get
            {
                return this.Text.Replace(Environment.NewLine, "<br />");
            }
        }
    }
}
