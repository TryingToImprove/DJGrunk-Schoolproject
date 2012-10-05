using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Exceptions
{
    public class NotEnoughFundsException : Exception
    {
        public int Funds { get; private set; }
        public int NeededFunds { get; private set; }

        public NotEnoughFundsException(string message, int funds, int neededFunds)
            : base(message)
        {
            this.Funds = funds;
            this.NeededFunds = neededFunds;
        }
    }
}
