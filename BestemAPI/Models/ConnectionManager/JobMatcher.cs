using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BestemAPI.Models.ConnectionManager;

namespace BestemAPI.Models
{
    public class JobMatcher
    {

        public static String connectionString = DbManager.connectionString;


        public List<Job> TryToMatch(Job newjob)
        {

            List<Job> matchList = new List<Job>();
            using (SqlConnection con = new SqlConnection(connectionString)) { // try to match the startlocatiion
                if (con.State == ConnectionState.Closed) con.Open();

                try {

                    String cmdString = @"SELECT Id,name,coord.Lat,coord.Long FROM Location WHERE coord.STDistance('POINT("
                                       + newjob.startLocation.lng + " " 
                                       + newjob.startLocation.lat + ")') < 25000 AND Id > 1298";

                    System.Diagnostics.Debug.Write(cmdString);
                    SqlCommand cmd = new SqlCommand(cmdString, con);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {

                            newjob.startLocation.Jobs.AddRange( JobManager.GetJobsForLocationId(Convert.ToInt32(reader[0])) );
                        }

                    }
                }
                catch {

                    throw;
                }

            }

            if(newjob.startLocation.Jobs != null) {
                using (SqlConnection con = new SqlConnection(connectionString)) { // try to match the endtlocatiion
                    if (con.State == ConnectionState.Closed) con.Open();

                    try {

                        String cmdString = @"SELECT Id,name,coord.Lat,coord.Long FROM Location WHERE coord.STDistance('POINT("
                                           + newjob.endLocation.lng + " "
                                           + newjob.endLocation.lat + ")') < 25000 AND Id > 1298";
                        SqlCommand cmd = new SqlCommand(cmdString, con);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {

                                newjob.endLocation.Jobs.AddRange(JobManager.GetJobsForLocationId(Convert.ToInt32(reader[0])));
                            }

                        }
                    }
                    catch {


                    }

                }

                if (newjob.endLocation.Jobs != null) {


                    foreach (Job j in newjob.endLocation.Jobs) {

                        System.Diagnostics.Debug.Write(j.jobID + "\n");
                    }

                    foreach (Job j in newjob.startLocation.Jobs) {

                        System.Diagnostics.Debug.Write(j.jobID + "\n");
                    }

                    matchList = newjob.endLocation.Jobs.Intersect(newjob.startLocation.Jobs).ToList();


                }


            }

            

            return matchList;
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
        double GetDistanceInKm(Location loc1, double latc, double longc)
        {
            int R = 6371; // Radius of the earth in km
            double dLat = deg2rad(latc - loc1.lat);
            double dLon = deg2rad(longc - loc1.lng);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(loc1.lat)) * Math.Cos(deg2rad(latc)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km


            return d;
        }



        public double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}