using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Security.Authentication
{
    public class UserData
    {
        private const string Delimeter = "||||||";

        public int CredentialsId { get; set; }
        public string UserName { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsSystemUser { get; set; }

        public override string ToString()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", Delimeter, this.CredentialsId, this.UserName, this.IsSystemUser, this.FirstName, this.LastName);
        }

        internal static bool TryParse(string data, out UserData adminData)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException("data");
            }

            adminData = null;

            string[] segments = data.Split(new string[] { Delimeter }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length > 5)
            {
                return false;
            }

            adminData = new UserData()
            {
                CredentialsId = int.Parse(segments[0]),
                UserName = segments[1],
                IsSystemUser = bool.Parse(segments[2]),
                FirstName = (segments.Length > 3) ? segments[3] : null,
                LastName = (segments.Length > 4) ? segments[4] : null,
            };

            return true;
        }
    }
}
