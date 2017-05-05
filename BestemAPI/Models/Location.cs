using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestemAPI.Models
{
    public class Location
    {


        public Location(int locationID, string name ,double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
            this.name = name;
            this.locationID = locationID;
            this.Jobs = new List<Job>();
        }

        public String name { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int locationID { get; set; }

        [JsonIgnore]
        public List<Job> Jobs { get; set; }
        
    }
}