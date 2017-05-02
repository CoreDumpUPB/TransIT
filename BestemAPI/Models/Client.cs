using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestemAPI.Models
{
    public class Client
    {
        public Client(int clientID, string name, string phoneNumber, string email)
        {
            this.clientID = clientID;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        public Client(int clientID, string name, string phoneNumber, string email, string password)
        {
            this.clientID = clientID;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
        }

        public int clientID { get; set; }
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public String password { get; set; }

    }
}