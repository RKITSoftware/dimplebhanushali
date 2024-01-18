using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Caching.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            var result = new string[] { "Dimple", "Mithiya" };
            CacheModel.Add("ID111", result);
            return result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            var result = CacheModel.Get("ID111");

            CacheModel.Remove("ID111");
            var result2 = CacheModel.Get("ID123");

            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
