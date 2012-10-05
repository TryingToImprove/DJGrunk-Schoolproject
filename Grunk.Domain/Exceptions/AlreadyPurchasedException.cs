using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Exceptions
{
    public class AlreadyPurchasedException : Exception
    {
        public int ProductId { get; set; }

        public AlreadyPurchasedException(string message, int productId)
            : base(message)
        {
            this.ProductId = productId;
        }
    }
}
