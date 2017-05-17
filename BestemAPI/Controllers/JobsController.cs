using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestemAPI.Models;
using BestemAPI.Models.ConnectionManager;

namespace BestemAPI.Controllers
{
    public class JobsController : ApiController
    {
        // GET: api/Jobs


        [Route("api/Jobs/GetByUserId/{id}")]
        public IEnumerable<Job> GetByUserId(int id)
        {
            return JobManager.GetJobsByUserId(id);
        }


        [Route("api/Jobs/GetAll/")]
        public IEnumerable<Job> GetAll() {

            return JobManager.GetAllJobs();

        }

   

        [Route("api/Jobs/")]
        public HttpResponseMessage Post( [FromBody] Job job) {
            try {


                job.startLocation.locationID = LocationManager.insertLocation(job.startLocation);
                job.endLocation.locationID = LocationManager.insertLocation(job.endLocation);


                


                JobManager.insertJob(job);

                
                JobMatcher jobMatcher = new JobMatcher();
                job.MatchedJobs = jobMatcher.TryToMatch(job);

                System.Diagnostics.Debug.Write("Job matching test:");
                foreach ( Job j in job.MatchedJobs) {

                    System.Diagnostics.Debug.Write(j.jobID + "\n");
                }

                return Request.CreateResponse(HttpStatusCode.Created,job);
            }
            catch(Exception ex) {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);

            }

        }

        // PUT: api/Jobs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Jobs/5
        public void Delete(int id)
        {
        }


        /*   public IEnumerable<Job> Get(int userID, float lat1, float long1, float lat2, float long2, int transportType, int TransportMethod)
      {
          Location l1 = LocationManager.searchLocation(lat1, long1);
          Location l2 = LocationManager.searchLocation(lat2, long2);
         // LocationManager.insertIntermediateLocations(userID, l1, l2);
          JobManager.insertJob(new Job(-1, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, 1, l1.locationID, l2.locationID));

          return null;
        // return JobManager.getJobs(userID, lat1, long1, lat2, long2, 0);
          //Job job = new Job(-1,0, 0, transportType,0,0,DateTime.Now, DateTime.Now,userID, new Location(lat1, long1), new Location(lat2, long2));
      }*/


        // POST: api/Jobs
        /*   public void Post([FromUri] int type, [FromUri] int status, [FromUri] int vehicle,
               [FromUri] float capacity, [FromUri] float price, [FromUri] DateTime start_date, [FromUri] DateTime end_date,
               [FromUri] int userid, [FromUri] , [FromUri] int end_loc  )
           {
               ConnectionManager.insertJob(new Job(-1, type, status, vehicle, capacity, price, start_date, end_date, userid,start_loc, end_loc));
           }*/
    }
}
