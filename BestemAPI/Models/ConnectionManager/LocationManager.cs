using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {
    public static class LocationManager {

        private static SqlConnection connection = DbManager.getConnection();

        public static Location getLocation(int LocationID) {
            Location loc = null;
            try {
                connection.Open();
                String cmdString = "Select * From [dbo].[Location] Where Id=" + LocationID;
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        loc = new Location(Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2]), LocationID);
                    }
                }
                connection.Close();
                return loc;
            }
            catch { return loc; }

        }

        public static void insertLocation(List<Location> locationList) {
            SqlConnection connection = DbManager.getConnection();
            try {
                connection.Open();
                foreach (Location loc in locationList) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into [dbo].[Location](name, lat, long) Values('");
                    sb.Append(loc.name + "',");
                    sb.Append(loc.lat + ",");
                    sb.Append(loc.lng + ")");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), connection);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch { connection.Close(); }
        }



        public static Location searchLocation(float lat1, float long1, SqlConnection connection) {
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

        }
        public static Location searchLocation(float lat1, float long1) {

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

        }

        public static int insertIntermediateLocations(int ClientID, Location startLocation, Location endLocation) {
            LocationLoader loader = new LocationLoader();
            List<Location> locs = loader.getIntermediateLocations(startLocation, endLocation);
            insertLocation(locs);
            return 0;
        }

        public static Location insertLocationbyCoord(float lat1, float long1) {
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

        }
    }
}