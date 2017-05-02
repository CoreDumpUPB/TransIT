using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BestemAPI.Models
{
    public class JobMatcher
    {
        public bool match(Job newjob)
        { // match job with sql db


            //parcurge toate locatiile si daca start_location @ found_locid si end_location @ found_locid2
            // daca found_locid si found_locid2 au ac job din jli => MATCH
            //select form jli where locid = locid 


            String connectionString = "Server=tcp:transapp.database.windows.net,1433;Initial Catalog=trans-db;Persist Security Info=False;User ID=transapp-admin;Password=Coredump123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Id,lat,long FROM Location";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            int startmatchid = 0, endmatchid = 0;

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (GetDistanceInKm(ConnectionManager.getLocation(newjob.startLocationID), Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2])) < 30)
                    {
                        startmatchid = Convert.ToInt32(reader[0]);

                    }
                    if (GetDistanceInKm(ConnectionManager.getLocation(newjob.endLocationID), Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2])) < 30)
                    {
                        endmatchid = Convert.ToInt32(reader[0]);

                    }


                }
            }

            if (startmatchid != 0 && endmatchid != 0)
            {
                cmd.CommandText = "SELECT jobid FROM Jli WHERE locid=" + Convert.ToString(startmatchid);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                int startmatchjob = Convert.ToInt32(reader[0]);

                cmd.CommandText = "SELECT jobid FROM Jli WHERE locid=" + Convert.ToString(endmatchid);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                reader = cmd.ExecuteReader();
                reader.Read();

                int endmatchjob = Convert.ToInt32(reader[0]);

                if (endmatchjob == startmatchjob)
                {
                    cmd.CommandText = "Insert Into MatchedJobs(job1, job2) Values(" + newjob + "," + endmatchjob + ")";
                    // add to matched table endmatchjob + newjob as new entry
                    // set jobs status to matched
                    cmd.ExecuteNonQuery();

                }
            }
            sqlConnection1.Close();

            return true;
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