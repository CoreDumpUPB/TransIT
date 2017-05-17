using BestemAPI.Models.ConnectionManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestemAPI.Models
{
    public class Job
    {
        public Job(int jobID, int transportType, int status, int transportMethod, float capacity, float price, DateTime startDate, DateTime endDate, int userID, Location startLocation, Location endLocation)
        {
            this.jobID = jobID;
            this.transportType = transportType;
            this.status = status;
            this.transportMethod = transportMethod;
            this.capacity = capacity;
            this.price = price;
            this.startDate = startDate;
            this.endDate = endDate;
            this.userID = userID;
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            Locations = new List<Location>();



        }

        public int jobID { get; set; }
        public int transportType { get; set; }
        public int status { get; set; }
        public int transportMethod { get; set; }
        public float capacity { get; set; }
        public float price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int userID { get; set; }
        public Location startLocation { get; set; }
        public Location endLocation { get; set; }

        [JsonIgnore]
        public List<Job> MatchedJobs {get; set; }

       [JsonIgnore]
        public List<Location> Locations { get; set; }


        public override int GetHashCode() {
            return this.jobID;
        }

        public override bool Equals(object other) {
            if (other is Job)
                return ((Job)other).jobID == this.jobID;
            return false;
        }
    }
}