using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Grunk.Security;

namespace Grunk.Web.Models
{
    public abstract class BaseViewModel
    {
        private IEnumerable<MenuItem> MenuItems { get; set; }
        public string MenuKey
        {
            get { return MenuKey; }
            set
            {
                MenuItem selectedMenuItem = MenuItems.FirstOrDefault(x => x.Key.Equals(value, StringComparison.InvariantCultureIgnoreCase));
                if (selectedMenuItem != null)
                {
                    selectedMenuItem.Selected = true;
                }
            }
        }
        public ICustomPrincipal ProfileData { get; set; }

        public BaseViewModel()
        {
            this.MenuItems = GenerateMenuItems();
        }
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return MenuItems;
        }

        private static IEnumerable<MenuItem> GenerateMenuItems()
        {
            IEnumerable<MenuItem> menuItems = new MenuItem[]{
                new MenuItem{
                    Key = "home",
                    Name = "Forside",
                    Action = "Index",
                    Controller = "Home"
                },
                new MenuItem{
                    Key = "shop",
                    Name = "Musikbutikken",
                    Action = "Index",
                    Controller = "Shop"
                },
                new MenuItem{
                    Key = "reviews",
                    Name = "Anmeldelser",
                    Action = "Index",
                    Controller = "Reviews"
                },
                new MenuItem{
                    Key = "profile",
                    Name = "Min profil",
                    Action = "Index",
                    Controller = "Profile"
                },
                new MenuItem{
                    Key = "contact",
                    Name = "Kontakt",
                    Action = "Index",
                    Controller = "Contact"
                }
            };

            return menuItems;
        }

        public class MenuItem
        {
            public string Key { get; set; }
            public string Name { get; set; }

            public bool Selected { get; set; }

            public string Action { get; set; }
            public string Controller { get; set; }
            public RouteValueDictionary Parameters { get; set; }

            public MenuItem()
            {
                this.Parameters = null;
            }
        }
    }
}