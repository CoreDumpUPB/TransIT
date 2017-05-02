using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestemAPI.Models
{
    public class Location
    {
        public Location(double latitude, double longitude)
        {
            this.lat = latitude;
            this.lng = longitude;
        }

        public Location(double lat, double lng, int locationID)
        {
            this.lat = lat;
            this.lng = lng;
            this.locationID = locationID;
        }

        public String name { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int locationID { get; set; }
    }
}