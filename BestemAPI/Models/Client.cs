using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestemAPI.Models
{
    public class Client
    {
        public Client(int clientID, string name, string phoneNumber, string email, List<Job> jobList)
        {
            this.clientID = clientID;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.Jobs = jobList;
        }

        public Client(int clientID, string name, string phoneNumber, string email, string password, List<Job> jobList)
        {
            this.clientID = clientID;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
            this.Jobs = jobList;
        }

        public int clientID { get; set; }
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public String password { get; set; }

        public List<Job> Jobs { get; set; } // every client has more jobs

    }
}