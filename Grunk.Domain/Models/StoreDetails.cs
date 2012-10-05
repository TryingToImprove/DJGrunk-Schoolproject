using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Grunk.Domain
{
    public class StoreDetails
    {
        public StoreDetails()
        {
            this.OpeningHours = new List<OpeningHour>();
            this.Address = new AddressInformation();
            this.Contact = new ContactInformation();
        }

        public string Name { get; set; }
        public AddressInformation Address { get; set; }
        public ContactInformation Contact { get; set; }

        public List<OpeningHour> OpeningHours { get; set; }

        public OpeningHour GetOpenHourByDayIndex(int index)
        {
            return this.OpeningHours.Find(x => x.Day.Equals(index));
        }

        public List<string> GetPrettyOpeningHours()
        {
            if (this.OpeningHours == null || this.OpeningHours.Count < 1)
            {
                return null;
            }

            List<string> returnList = new List<string>();

            IEnumerable<OpeningHour> orderedHours = this.OpeningHours.Where(x => x.Closeing >= 0 && x.Opening >= 0).OrderBy(x => x.Day);

            IEnumerable<OpeningHour> groupedHours = orderedHours.GroupBy(x => new { x.Opening, x.Closeing })
                .Select(group => new OpeningHour
                {
                    Closeing = group.Key.Closeing,
                    Opening = group.Key.Opening
                });

            foreach (var groupedHour in groupedHours)
            {
                var dayNames = CultureInfo.GetCultureInfo("da-dk").DateTimeFormat.DayNames;

                var hour = orderedHours.FirstOrDefault(x => x.Closeing == groupedHour.Closeing && x.Opening == groupedHour.Opening);

                if (orderedHours.Count(x => x.Closeing == hour.Closeing && x.Opening == hour.Opening) > 1)
                {
                    var startDay = orderedHours.FirstOrDefault(x => x.Closeing == groupedHour.Closeing && x.Opening == groupedHour.Opening);
                    var endDay = orderedHours.LastOrDefault(x => x.Closeing == groupedHour.Closeing && x.Opening == groupedHour.Opening);

                    returnList.Add(string.Format("<span class=\"days\">{0}-{3}</span> <span class=\"time\">{1:00:00}-{2:00:00}</span>", dayNames[startDay.Day], hour.Opening, hour.Closeing, dayNames[endDay.Day]));
                }
                else
                {
                    returnList.Add(string.Format("<span class=\"days\">{0}</span>  <span class=\"time\">{1:00:00}-{2:00:00}</span>", dayNames[hour.Day], hour.Opening, hour.Closeing));
                }
            }

            return returnList;
        }

        public class OpeningHour
        {
            public int Opening { get; set; }
            public int Closeing { get; set; }
            public int Day { get; set; } //Reprensent a day as a integar (man = 1, tirs= 2, ons = 3, osv.)
        }

        public class ContactInformation
        {
            public string Telephone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
        }

        public class AddressInformation
        {
            public int Number { get; set; }
            public string Road { get; set; }
            public int Postal { get; set; }
            public string Town { get; set; }
        }
    }
}
