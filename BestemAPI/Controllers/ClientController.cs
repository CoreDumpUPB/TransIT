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
