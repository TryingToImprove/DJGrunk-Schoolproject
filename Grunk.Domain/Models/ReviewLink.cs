using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain
{
    public partial class ReviewLink
    {
        public string FullUrl
        {
            get
            {
                string url = this.Url;
                if (!url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                {
                    url = "http://" + url;
                }

                return url;
            }
        }

        public string PrettyUrl
        {
            get
            {
                string url = this.Url;
                if (url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                {
                    url = url.Remove(0, 7);
                }
                if (!url.StartsWith("www."))
                {
                    url = "www." + url;
                }

                return url;
            }
        }
    }
}
