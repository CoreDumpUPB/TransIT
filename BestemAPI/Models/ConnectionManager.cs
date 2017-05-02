using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BestemAPI.Models
{

    public class ConnectionManager
    {
       public static SqlConnection connection = null;

       static  String connectionString = "Server=tcp:transapp.database.windows.net,1433;Initial Catalog=trans-db;Persist Security Info=False;User ID=transapp-admin;Password=Coredump123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

       public static SqlConnection getConnection()
        {
            if(connection == null)
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                }
                catch
                {   
                }
            }

            return connection;
        }


        public static Object insertClient(Client client)
        {
            if (client == null)
                return "Wrong parameters";
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into [dbo].[User](nume, phone, email, password) ");
                sb.Append("Output INSERTED.Id ");
                sb.Append("Values ('");
                sb.Append(client.name);
                sb.Append("' ,'");
                sb.Append(client.phoneNumber);
                sb.Append("' ,'");
                sb.Append(client.email);
                sb.Append("' ,'");
                sb.Append(client.password);
                sb.Append("' )");
                SqlCommand cmd = new SqlCommand(sb.ToString(), connection);

                client.clientID = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                return client.clientID;
            }
            catch { connection.Close(); return "Error during database connection"; }

        }

        public static Client getClientbyEmail(String email)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {


                connection.Open();
                String cmdString = "Select * From [dbo].[User] Where email='" + email +  "'";

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                Client client = null;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }
                }

                connection.Close();
                return client;
            }
            catch { connection.Close(); return null; }

        }

        public static Client getClientbyEmail(String email, String password)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {


                connection.Open();
                String cmdString = "Select * From [dbo].[User] Where email='" + email+"' AND password='" + password + "'" ;

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                Client client = null;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }
                }
        
                connection.Close();
               return client;
            }
            catch { connection.Close(); return null; }
           
        }

        public static Client getClientbyID(int ID)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {
                connection.Open();
                String cmdString = "Select * From User Where Id=" + ID;

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                Client client = null;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }
                }

                connection.Close();
                return client;
            }
            catch { return null; }
            return null;
        }

        public static Job[] getJob(Job clientTrip)
        {
            return null;
        }

        public static IEnumerable<Job> getJobs()
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {
                connection.Close();
            }
            catch { }
            List<Job> jobs = new List<Job>();
            try
            {


                connection.Open();
                String cmdString = "Select * From [dbo].[JOB] ";

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobs.Add(new Job(Convert.ToInt32(reader[0]),
                           Convert.ToInt32(reader[1]),
                           Convert.ToInt32(reader[2]),
                           Convert.ToInt32(reader[3]),
                           (float)Convert.ToDouble(reader[4]),
                           (float)Convert.ToDouble(reader[5]),
                           DateTime.Parse(reader[6].ToString()),
                           DateTime.Parse(reader[7].ToString()),
                           Convert.ToInt32(reader[8]),
                           Convert.ToInt32(reader[9]),
                           Convert.ToInt32(reader[10])
                           ));
                    }
                }

                connection.Close();
                return jobs;
            }
            catch { connection.Close(); return jobs; }
        }

        public static int insertIntermediateLocations(int ClientID, Location startLocation, Location endLocation)
        {
            LocationLoader loader = new LocationLoader();
            List<Location> locs = loader.getIntermediateLocations(startLocation, endLocation);
            insertLocation(locs);
            return 0;
        }


        public static IEnumerable<Job> getJobsById(int id)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            connection.Close();
            try
            {


                connection.Open();
                String cmdString = "Select * From [dbo].[JOB] Where UserID=" + id.ToString();

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                List<Job> jobs = new List<Job>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobs.Add(new Job(Convert.ToInt32(reader[0]),
                           Convert.ToInt32(reader[1]),
                           Convert.ToInt32(reader[2]),
                           Convert.ToInt32(reader[3]),
                           (float)Convert.ToDouble(reader[4]),
                           (float)Convert.ToDouble(reader[5]),
                           DateTime.Parse(reader[6].ToString()),
                           DateTime.Parse(reader[7].ToString()),
                           Convert.ToInt32(reader[8]),
                           Convert.ToInt32(reader[9]),
                           Convert.ToInt32(reader[10])
                           ));
                    }
                }

                connection.Close();
                return jobs;
            }
            catch { connection.Close(); return null; }
        }

        public static void insertJob(Job job)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into [dbo].[User](nume, phone, email, password) ");
                sb.Append("Values ("+job.transportType);
                sb.Append("," + job.status);
                sb.Append("," + job.transportMethod);
                sb.Append("," + job.status);
                sb.Append("," + job.capacity);
                sb.Append("," + job.price);
                sb.Append("," + job.startDate);
                sb.Append("," + job.endDate);
                sb.Append("," + job.userID);
                sb.Append("," + job.startLocationID);
                sb.Append("," + job.endLocationID);
                sb.Append("' )");
                SqlCommand cmd = new SqlCommand(sb.ToString(), connection);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }

        public static Location getLocation(int LocationID)
        {
            Location loc = null;
            try
            {
                connection.Open();
                String cmdString = "Select * From [dbo].[Location] Where Id=" + LocationID;
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loc = new Location(Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2]), LocationID);
                    }
                }
                connection.Close();
                return loc;
            }
            catch { return loc; }
            
        }

        public static void insertLocation(List<Location> locationList)
        {
            SqlConnection connection = ConnectionManager.getConnection();
            try
            {
                connection.Open();
                foreach (Location loc in locationList)
                {
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

        public static List<Job> getJobs(int userID, float lat1, float long1, float lat2, float long2, int transportType)
        {
            try
            {
                connection.Open();
                Location loc1 = searchLocation(lat1, long1, connection);
                Location loc2 = searchLocation(lat2, long2, connection);


                List<Job> job = new List<Job>();
                
                connection.Close();
            }
            catch { }
            return null;
        }
    
        public static  Location searchLocation(float lat1, float long1, SqlConnection connection)
        {
            Location loc = null;
            String cmdString = "Select * From [dbo].[Location] Where lat=" + lat1;
            try
            {
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loc = new Location(lat1, long1, Convert.ToInt32(reader[0]));
                    }
                    else
                    {
                        cmdString = "Insert Into [dbo].[Location](lat, long) Values(" + lat1+"," + long1 + ")";
                        cmd = new SqlCommand(cmdString, connection);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch { }

            return loc;

        }
        public static Location searchLocation(float lat1, float long1)
        {
            
            Location loc = null;
            SqlConnection connection = ConnectionManager.getConnection();
            connection.Open();
            String cmdString = "Select * From [dbo].[Location] Where lat=" + lat1;
         //   try
            {
                SqlCommand cmd = new SqlCommand(cmdString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loc = new Location(lat1, long1, Convert.ToInt32(reader[0]));
                        connection.Close();
                    }
                    else
                    {
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
        //   public static IEnumerable<Job> getCloseJobs(Location startLocation, Location endLocation)
        //{
        //   List<Job> jobList;

        //}




    }
}