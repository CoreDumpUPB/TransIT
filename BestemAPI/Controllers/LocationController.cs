using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestemAPI.Models;

namespace BestemAPI.Controllers
{
    public class LocationController : ApiController
    {
        // GET: api/Location
        public IEnumerable<Location> Get()
        {
            Location ltest = new Location(10,"test1",44.434798f, 26.102869f);
            Location ltest2 = new Location(10,"tet2",45.272648f, 27.966757f);
            LocationLoader loader = new LocationLoader();
            List<Location> list = loader.getIntermediateLocations(ltest, ltest2);
            return list;
        }

        // GET: api/Location/5
        public IEnumerable<Location> Get(int id)
        {
            Location ltest = new Location(10, "test1",44.434798f, 26.102869f);
            Location ltest2 = new Location(10, "tet2", 45.272648f, 27.966757f);
            LocationLoader loader = new LocationLoader();
            List<Location> list = loader.getIntermediateLocations(ltest, ltest2);
            return list;
        }


        // POST: api/Location
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Location/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Location/5
        public void Delete(int id)
        {
        }
    }
}
