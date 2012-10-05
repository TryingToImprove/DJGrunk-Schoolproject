using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain;

namespace Grunk.Web.Helpers
{
    public static class ReviewStateHelper
    {
        public static string GetDanishName(this ReviewState state)
        {
            switch (state)
            {
                case ReviewState.Approved:
                    return "Godkendt";
                case ReviewState.NotApproved:
                    return "Afvist";
                case ReviewState.WaitingForApproval:
                    return "Venter på godkendelse";
                default:
                    return state.ToString();
            }
        }
    }
}
