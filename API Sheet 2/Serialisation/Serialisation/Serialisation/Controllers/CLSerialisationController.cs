using Serialisation.BL;
using Serialisation.Models;
using System.Web.Http;
using System.Xml.Linq;

namespace Serialisation.Controllers
{
    /// <summary>
    /// Controller class for demonstrating serialization and deserialization operations.
    /// </summary>
    public class CLSerialisationController : ApiController
    {
        /// <summary>
        /// Converts a Member object to a JSON string.
        /// </summary>
        [HttpGet]
        [Route("api/JsonToString")]
        public IHttpActionResult ConvertJsonToString()
        {
            Member objMember = new Member
            {
                Age = 23,
                Name = "Dimple"
            };

            string strJson = BLSerialisation.ConvertObjectToJson(objMember);
            return Ok(strJson);
        }

        /// <summary>
        /// Converts a JSON string to a Member object.
        /// </summary>
        [HttpPost]
        [Route("api/StringToJson")]
        public IHttpActionResult StringToJson([FromBody] string strJson)
        {
            Member objMember = BLSerialisation.ConvertJsonToObject(strJson);
            return Ok(objMember);
        }

        /// <summary>
        /// Converts a Member object to an XML element.
        /// </summary>
        [HttpPost]
        [Route("api/JsonToXml")]
        public IHttpActionResult JsonToXml([FromBody] Member json)
        {
            XElement rootElement = BLSerialisation.ConvertJsonToXml(json);
            return Ok(rootElement);
        }

        /// <summary>
        /// Converts an XML element to a Member object.
        /// </summary>
        [HttpPost]
        [Route("api/XmlToJson")]
        public IHttpActionResult XmlToJson([FromBody] XElement root)
        {
            Member objMember = BLSerialisation.ConvertXmlToJson(root);
            return Ok(objMember);
        }

        /// <summary>
        /// Converts an XML element to a JSON string.
        /// </summary>
        [HttpPost]
        [Route("api/XmlToString")]
        public IHttpActionResult XmlToString([FromBody] XElement root)
        {
            return Ok(BLSerialisation.ConvertXmlToString(root));
        }

        /// <summary>
        /// Converts a JSON string to an XML element.
        /// </summary>
        [HttpPost]
        [Route("api/StringToXml")]
        public IHttpActionResult StringToXml([FromBody] string strJson)
        {
            XElement xmlOutput = BLSerialisation.ConvertStringToXml(strJson);
            return Ok(xmlOutput);
        }
    }
}
