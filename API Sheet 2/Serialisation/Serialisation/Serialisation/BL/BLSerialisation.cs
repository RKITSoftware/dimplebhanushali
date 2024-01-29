using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serialisation.Models;
using System.Xml.Linq;

namespace Serialisation.BL
{
    /// <summary>
    /// Business Logic class for handling serialization and deserialization operations.
    /// </summary>
    public static class BLSerialisation
    {
        /// <summary>
        /// Converts a Member object to a JSON string.
        /// </summary>
        public static string ConvertObjectToJson(Member objMember)
        {
            return JsonConvert.SerializeObject(objMember);
        }

        /// <summary>
        /// Converts a JSON string to a Member object.
        /// </summary>
        public static Member ConvertJsonToObject(string strJson)
        {
            return JsonConvert.DeserializeObject<Member>(strJson);
        }

        /// <summary>
        /// Converts a Member object to an XML element.
        /// </summary>
        public static XElement ConvertJsonToXml(Member json)
        {
            XElement rootElement = new XElement("root",
                   new XElement("Name", json.Name),
                   new XElement("Age", json.Age)
               );
            return rootElement;
        }

        /// <summary>
        /// Converts an XML element to a Member object.
        /// </summary>
        public static Member ConvertXmlToJson(XElement root)
        {
            string nameValue = root.Element("Name")?.Value;
            int ageValue = int.Parse(root.Element("Age")?.Value);

            return new Member
            {
                Name = nameValue,
                Age = ageValue
            };
        }

        /// <summary>
        /// Converts an XML element to a JSON string.
        /// </summary>
        public static string ConvertXmlToString(XElement root)
        {
            return JsonConvert.SerializeXNode(root);
        }

        /// <summary>
        /// Converts a JSON string to an XML element.
        /// </summary>
        public static XElement ConvertStringToXml(string strJson)
        {
            XElement jsonOutput = JsonConvert.DeserializeXNode($"{{\"Root\":{strJson}}}").Root;
            return jsonOutput;
        }
    }
}
