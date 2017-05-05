using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {
    public static class LocationManager {

        public static String connectionString = DbManager.connectionString;

        public static Location GetLocationById(int LocationID) {
            Location loc = null;
            System.Diagnostics.Debug.Write("WTF DCC");
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();

                try {

                    String cmdString = "Select * From [Location] Where Id=" + LocationID;


                    SqlCommand cmd = new SqlCommand(cmdString, con);



                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            loc = new Location(LocationID, reader[1].ToString(), Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3]));
                            
                        }

                    }
                }
                 catch {}
      


            }
            return loc;
        }

        public static List<Location> GetLocationsForJob(Job job) {
            List<Location> locationList = new List<Location>();
            System.Diagnostics.Debug.Write("WTF DCC");
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();

                try {

                    String cmdString = "Select * From [JLI] Where jobid=" + job.jobID;


                    SqlCommand cmd = new SqlCommand(cmdString, con);



                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            System.Diagnostics.Debug.Write(String.Format("Adaug locatia {0}", Convert.ToInt32(reader[2])));
                            locationList.Add(GetLocationById(Convert.ToInt32(reader[2])));

                        }

                    }
                }
                catch { }



            }
            return locationList;
        }


        public static List<Location> ComputeLocationsForJob(Job job) {

            List<Location> locationList = new List<Location>();

            LocationLoader locLoad = new LocationLoader();
            locationList = locLoad.getIntermediateLocations(job.startLocation, job.endLocation);

            return locationList;
        }

        public static void insertLocations(List<Location> locationList) {

            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();
                try {
                    
                    foreach (Location loc in locationList) {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Insert into [dbo].[Location](name, lat, long) Values('");
                        sb.Append(loc.name + "',");
                        sb.Append(loc.lat + ",");
                        sb.Append(loc.lng + ")");
                        SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                        cmd.ExecuteNonQuery();
                    }
                  

                }
                catch { }
            }
            
        }

        public static void insertLocation(Location loc) {

            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();
                try {

                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into [dbo].[Location](name, lat, long) Values('");
                    sb.Append(loc.name + "',");
                    sb.Append(loc.lat + ",");
                    sb.Append(loc.lng + ")");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                    cmd.ExecuteNonQuery();
                    


                }
                catch { }
            }

        }



        /* public static Location searchLocation(float lat1, float long1, SqlConnection connection) {
             Location loc = null;
             String cmdString = "Select * From [dbo].[Location] Where lat=" + lat1;
             try {
                 SqlCommand cmd = new SqlCommand(cmdString, connection);
                 using (SqlDataReader reader = cmd.ExecuteReader()) {
                     if (reader.Read()) {
                         loc = new Location(lat1, long1, Convert.ToInt32(reader[0]));
                     }
                     else {
                         cmdString = "Insert Into [dbo].[Location](lat, long) Values(" + lat1 + "," + long1 + ")";
                         cmd = new SqlCommand(cmdString, connection);
                         cmd.ExecuteNonQuery();

                     }
                 }
             }
             catch { }

             return loc;

         }*/
        /*public static Location searchLocation(float lat1, float long1) {

            Location loc = null;
            SqlConnection connection = DbManager.getConnection();
            connection.Open();
            String cmdString = "Select * From [dbo].[Location] Where lat=" + lat1;
            //   try
            {
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        loc = new Location(lat1, long1, Convert.ToInt32(reader[0]));
                        connection.Close();
                    }
                    else {
                        reader.Close();
                        cmdString = "Insert Into [dbo].[Location](lat, long) Values(" + lat1 + "," + long1 + ")";
                        cmd = new SqlCommand(cmdString, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        loc = searchLocation(lat1, long1);
                    }
                }
            }
            //  catch { }
            //finally
            {
                connection.Close();
            }
            return loc;

        }*/



        /*public static Location insertLocationbyCoord(float lat1, float long1) {
            Location loc = null;

            SqlConnection connection = DbManager.getConnection();
            connection.Open();
            String cmdString = "Select * From [dbo].[Location] Where lat=" + lat1;
            try {
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        loc = new Location(lat1, long1, Convert.ToInt32(reader[0]));
                    }
                    else {
                        cmdString = "Insert Into [dbo].[Location](lat, long) Values(" + lat1 + "," + long1 + ")";
                        cmd = new SqlCommand(cmdString, connection);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch {

            }
            finally {
                connection.Close();
            }

            return loc;

        }*/
    }
}