using System.Globalization;
using System.Web.Mvc;

namespace Grunk.Web.Helpers
{
    public static class DayHelper
    {
        public static string TranslateEnglishDay(this HtmlHelper htmlHelper, int number, string cultureName)
        {
            string[] dayNames = CultureInfo.GetCultureInfo(cultureName).DateTimeFormat.DayNames;

            return dayNames[number];
        }
    }
}
