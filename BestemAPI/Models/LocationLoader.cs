using BestemAPI.Models.ConnectionManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BestemAPI.Models
{
    public class LocationLoader
    {

        string API_KEY;  //google api key

        public LocationLoader()
        {
            Console.WriteLine("Location loader loaded!");
            API_KEY = "AIzaSyDypthjFsHdj1rneXHwo3mbAR45xRFqTpA";
        }



        public List<Location> getIntermediateLocations(Location start, Location end)
        {

            
            List<Location> list = new List<Location>();
           // list.Add(start);
            //list.Add(end);
            if (GetDistanceInKm(start, end) > 3000) return list;

            var request = (HttpWebRequest)WebRequest.Create(ComputeRequestUrl(start, end));

            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var json = JObject.Parse(rawJson);
            MapItem test = JsonConvert.DeserializeObject<MapItem>(rawJson);

            foreach (Route route in test.routes)
            {
                foreach (Leg leg in route.legs)
                {

                    list.Add(leg.start_location);
                    foreach (Step step in leg.steps)
                    {
                        if ( !IsTooClose(list[list.Count - 1], step.start_location) )
                        {
                            list.Add(step.start_location);
                       
                        }

                    }
                    list.Add(leg.end_location);
                }
            }


            return list;

        }

        private string ComputeRequestUrl(Location start, Location end)
        {



            string request = "https://maps.googleapis.com/maps/api/directions/json?origin=";
            request += Convert.ToString(start.lat) + "," + Convert.ToString(start.lng); // origin added
            request += "&";
            request += "destination=" + Convert.ToString(end.lat) + "," + Convert.ToString(end.lng); //destination added
            request += "&";
            request += "key=" + API_KEY;


            
            System.Diagnostics.Debug.Write(request);

            return request;



        }


        public bool IsTooClose(Location lastloc, Location loc)
        {


            if (GetDistanceInKm(lastloc, loc) < 2) {

                System.Diagnostics.Debug.Write("Is too close SESSION!");
                return true;
            }

            return false;

        }

        double GetDistanceInKm(Location loc1, Location loc2)
        {
            int R = 6371; // Radius of the earth in km
            double dLat = deg2rad(loc2.lat - loc1.lat);
            double dLon = deg2rad(loc2.lng - loc1.lng);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(loc1.lat)) * Math.Cos(deg2rad(loc2.lat)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km


            return d;
        }

        double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }


    }
}


