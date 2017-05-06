using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using BestemAPI.Models.ConnectionManager;
using System.Data;

namespace BestemAPI.Models.ConnectionManager {

    public static class JobManager {

        public static String connectionString = DbManager.connectionString;

        public static void insertJob(Job job) {
      
            
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();
                try {
          
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into [dbo].[JOB](type, status, vehicle, capacity , price ,start_date ,end_date ,userid ,start_loc ,end_loc) ");
                    sb.Append("OUTPUT Inserted.Id ");
                    sb.Append("Values (" + job.transportType);
                    sb.Append("," + job.status);
                    sb.Append("," + job.transportMethod);
                    sb.Append("," + job.capacity);
                    sb.Append("," + job.price);
                    sb.Append(",'" + job.startDate);
                    sb.Append("','" + job.endDate);
                    sb.Append("'," + job.userID);
                    sb.Append("," + job.startLocation.locationID);
                    sb.Append("," + job.endLocation.locationID);
                    sb.Append(")");

                    


                    SqlCommand cmd = new SqlCommand(sb.ToString(), con);

                    job.jobID = Convert.ToInt32(cmd.ExecuteScalar());



                    job.Locations = LocationManager.ComputeLocationsForJob(job); // not tested


                    insertJobLocations(job);


                }
                catch(Exception exp) {
                    System.Diagnostics.Debug.Write(exp.Message);
                }

            }
            
        }

        private static void insertJobLocations(Job job) {

            
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();
                try {

                    foreach (Location loc in job.Locations) {


                        loc.locationID = LocationManager.insertLocation(loc);

                        StringBuilder sb = new StringBuilder();
                        sb.Append("Insert into [dbo].[JLI](jobid,locid) ");
                        sb.Append("Values (" + job.jobID);
                        sb.Append("," + loc.locationID);
                        sb.Append(" )");

                        System.Diagnostics.Debug.Write(sb.ToString());
                        SqlCommand cmd = new SqlCommand(sb.ToString(), con);

                        cmd.ExecuteNonQuery();
                    }

                }
                catch { }

            }

        }



        public static List<Job> GetJobsByUserId(int userId) {


        
            List<Job> jobList = new List<Job>();


            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();


                try {

                    String cmdString = "Select * From [JOB] Where userid=" + userId;
                    SqlCommand cmd = new SqlCommand(cmdString, con);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {

                        while (reader.Read()) {

                            jobList.Add(new Job(Convert.ToInt32(reader[0]),
                               Convert.ToInt32(reader[1]),
                               Convert.ToInt32(reader[2]),
                               Convert.ToInt32(reader[3]),
                               (float)Convert.ToDouble(reader[4]),
                               (float)Convert.ToDouble(reader[5]),
                               DateTime.Parse(reader[6].ToString()),
                               DateTime.Parse(reader[7].ToString()),
                               Convert.ToInt32(reader[8]),
                               LocationManager.GetLocationById(Convert.ToInt32(reader[9])),
                               LocationManager.GetLocationById(Convert.ToInt32(reader[10]))
                               ));

                        }
                    }



                }
                catch {
                    //exception handle
                }
                finally {  }

            }

            return jobList;

        }

       /* public static List<Job> GetJobsForLocation(Location loc) {
            List<Job> jobList = new List<Location>();
            System.Diagnostics.Debug.Write("WTF DCC");
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();

                try {

                    String cmdString = "Select * From [JLI] Where locid=" + loc.locationID;


                    SqlCommand cmd = new SqlCommand(cmdString, con);



                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            
                            jobList.Add(GetJobById(Convert.ToInt32(reader[1])));

                        }

                    }
                }
                catch { }



            }
            return jobList;
        } */


        //inutile momentan:

        /*public static List<Job> getJobs(int userID, float lat1, float long1, float lat2, float long2, int transportType) {
             try {
                 SqlConnection connection = DbManager.getConnection();
                 connection.Open();
                 Location loc1 = searchLocation(lat1, long1, connection);
                 Location loc2 = searchLocation(lat2, long2, connection);


                 List<Job> job = new List<Job>();

                 connection.Close();
             }
             catch { }
             return null;
         } */

        /*public static IEnumerable<Job> getJobs() {
            //SqlConnection connection = DbManager.getConnection();
            try {
                DbManager.connection.Close();
            }
            catch { }
            List<Job> jobs = new List<Job>();
            try {


                DbManager.connection.Open();
                String cmdString = "Select * From [dbo].[JOB] ";

                SqlCommand cmd = new SqlCommand(cmdString, DbManager.connection);
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        jobs.Add(new Job(Convert.ToInt32(reader[0]),
                           Convert.ToInt32(reader[1]),
                           Convert.ToInt32(reader[2]),
                           Convert.ToInt32(reader[3]),
                           (float)Convert.ToDouble(reader[4]),
                           (float)Convert.ToDouble(reader[5]),
                           DateTime.Parse(reader[6].ToString()),
                           DateTime.Parse(reader[7].ToString()),
                           Convert.ToInt32(reader[8]),
                            LocationManager.GetLocationById(Convert.ToInt32(reader[9])),
                            LocationManager.GetLocationById(Convert.ToInt32(reader[10]))
                           ));
                    }
                }

                DbManager.connection.Close();
                return jobs;
            }
            catch { DbManager.connection.Close(); return jobs; }
        }*/


    }
}