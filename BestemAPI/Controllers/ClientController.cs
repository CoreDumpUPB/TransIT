using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Search;
using Newtonsoft.Json;
using BestemAPI.Models;


namespace BestemAPI.Controllers
{
    public class ClientController : ApiController
    {
        [HttpGet]
        // GET: api/Client
   
        public Object GetClientByID(int id)
        {
            Client client = ConnectionManager.getClientbyID(id);
            if (client == null)
            {

                return "client not found";
            }
            return client;
        }

        // GET: api/Client/{email}/{password}
        public Object GetClient( String email, String password)
        {
            Client client = ConnectionManager.getClientbyEmail(email, password);
            if(client == null)
            {
                return "client not found";
            }
            return client;
        }

        [Route ("api/Client/{email}/null")]
        public Object GetClientByEmail(String email)
        {
            Client client = ConnectionManager.getClientbyEmail(email);
            if (client == null)
            {

                return "client not found";
            }
            return client;
        }

        [HttpPost]
        // POST: api/Client
        public Object Post([FromUri] String email, [FromUri] String password, [FromUri] String name, [FromUri] String phoneNumber)
        {

            return ConnectionManager.insertClient(new Client(-1, name, phoneNumber, email, password));
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
