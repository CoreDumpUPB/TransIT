using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Search;
using Newtonsoft.Json;
using BestemAPI.Models;

using BestemAPI.Models.ConnectionManager;


namespace BestemAPI.Controllers
{
    public class ClientController : ApiController
    {
        [HttpGet]
        

        [Route("api/Client/GetById/{id}")]
        public Object GetClientByID(int id)
        {


           Client client = ClientManager.getClientbyID(id);
            if (client == null)
            {

                return "client not found";
            }


            //testing data
            /*  Location start = new Location(-1, "buc", 44.434366, 26.040221);
              Location end = new Location(-1, "buc2", 44.387665, 26.136360);


              DateTime startd = new DateTime(2000, 1, 3);
              DateTime endd = new DateTime(2000, 3, 3);
              Job job = new Job(-1, 1, 1, 1, 1, 1, startd, endd, 3, start, end);
              JobManager.insertJob(job);*/



            return client;


        }


        [Route("api/Client/GetByEmailAndPass/{email}/{password}")]

        public Object GetClient( String email, String password)
        {
            Client client = ClientManager.getClientbyEmail(email, password);
            if(client == null)
            {
                return "client not found";
            }
            return client;
        }



       
        [Route("api/Client/GetByEmail/{email}")]
        public Object GetClientByEmail(String email)
        {
            System.Diagnostics.Debug.Write("Request by email");
            Client client = ClientManager.getClientbyEmail(email);
            if (client == null)
            {

                return "client not found";
            }
            return client;
        }


       
        [Route("api/Client/GetJobsByEmail/{email}")]
        public Object GetClientJobsByEmail(String email) {
            Client client = ClientManager.getClientbyEmail(email);
            if (client.Jobs.Count == 0) {

                return "client has no jobs";
            }
            return client.Jobs;
        }




        [HttpPost]
        // POST: api/Client
        public Object Post([FromUri] String email, [FromUri] String password, [FromUri] String name, [FromUri] String phoneNumber)
        {

            //return ClientManager.insertClient(new Client(-1, name, phoneNumber, email, password,null));
            return null;
        }


        [HttpPut]
        // PUT: api/Client/5
        public void Put(int id, [FromBody]string value)
        {

        }

        [HttpDelete]
        // DELETE: api/Client/5
        public void Delete(int id)
        {
        }
    }
}
