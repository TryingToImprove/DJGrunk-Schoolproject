using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using System.Xml;
using Grunk.Domain;

namespace Grunk.DAL
{
    public class StoreDetailsRepository : IStoreDetailsRepository
    {
        XmlDocument Document;
        string XmlLocation;

        public StoreDetailsRepository(string xmlLocation)
        {
            this.XmlLocation = xmlLocation;

            this.Document = new XmlDocument();
            this.Document.Load(xmlLocation);
        }

        public StoreDetails GetStoreDetails()
        {
            StoreDetails storeDetails = new StoreDetails();

            XmlNode rootNode = Document.DocumentElement;

            storeDetails.Name = rootNode.SelectSingleNode("name").InnerText;

            MapAddressInformation(rootNode.SelectSingleNode("address"), ref storeDetails);
            MapContactInformation(rootNode.SelectSingleNode("contact"), ref storeDetails);
            MapOpeningHours(rootNode.SelectSingleNode("openinghours"), ref storeDetails);

            return storeDetails;
        }

        private void MapOpeningHours(XmlNode node, ref StoreDetails storeDetails)
        {
            storeDetails.OpeningHours = new List<StoreDetails.OpeningHour>();

            foreach (XmlNode n in node.ChildNodes)
            {
                if (!n.Name.Equals("openinghour")) continue;

                storeDetails.OpeningHours.Add(new StoreDetails.OpeningHour
                {
                    Day = int.Parse(n.Attributes["day"].InnerText),
                    Closeing = int.Parse(n.Attributes["closeing"].InnerText),
                    Opening = int.Parse(n.Attributes["opening"].InnerText)
                });
            }
        }

        private void MapAddressInformation(XmlNode node, ref StoreDetails storeDetails)
        {
            storeDetails.Address = new StoreDetails.AddressInformation
            {
                Number = int.Parse(node.SelectSingleNode("number").InnerText),
                Postal = int.Parse(node.SelectSingleNode("postal").InnerText),
                Road = node.SelectSingleNode("road").InnerText,
                Town = node.SelectSingleNode("town").InnerText
            };
        }

        private void MapContactInformation(XmlNode node, ref StoreDetails storeDetails)
        {
            storeDetails.Contact = new StoreDetails.ContactInformation
            {
                Email = node.SelectSingleNode("email").InnerText,
                Fax = node.SelectSingleNode("fax").InnerText,
                Telephone = node.SelectSingleNode("telephone").InnerText
            };
        }

        public void Update(StoreDetails storeDetails)
        {
            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder);

            writer.WriteStartElement("root");
            {
                writer.WriteStartElement("name");
                writer.WriteString(storeDetails.Name);
                writer.WriteEndElement();

                writer.WriteStartElement("address");
                {
                    writer.WriteStartElement("road");
                    writer.WriteString(storeDetails.Address.Road);
                    writer.WriteEndElement();

                    writer.WriteStartElement("town");
                    writer.WriteString(storeDetails.Address.Town);
                    writer.WriteEndElement();

                    writer.WriteStartElement("postal");
                    writer.WriteString(storeDetails.Address.Postal.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("number");
                    writer.WriteString(storeDetails.Address.Number.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("contact");
                {
                    writer.WriteStartElement("telephone");
                    writer.WriteString(storeDetails.Contact.Telephone);
                    writer.WriteEndElement();

                    writer.WriteStartElement("fax");
                    writer.WriteString(storeDetails.Contact.Fax);
                    writer.WriteEndElement();

                    writer.WriteStartElement("email");
                    writer.WriteString(storeDetails.Contact.Email);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("openinghours");
                {
                    foreach (var item in storeDetails.OpeningHours)
                    {
                        writer.WriteStartElement("openinghour");
                        writer.WriteAttributeString("day", item.Day.ToString());
                        writer.WriteAttributeString("opening", item.Opening.ToString());
                        writer.WriteAttributeString("closeing", item.Closeing.ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.Flush();
            writer.Close();

            string xml = builder.ToString();
            Document.LoadXml(xml);
            Document.Save(XmlLocation);
        }
    }
}
