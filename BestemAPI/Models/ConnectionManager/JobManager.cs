using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {

    public static class JobManager {

        public static void insertJob(Job job) {
            SqlConnection connection = DbManager.getConnection();
            try {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into [dbo].[User](nume, phone, email, password) ");
                sb.Append("Values (" + job.transportType);
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




        public static List<Job> GetJobsByUserId(int userId) {


            System.Diagnostics.Debug.Write("Trebuie sa dau joburile pt");
            List<Job> jobList = new List<Job>();

            SqlConnection connection = DbManager.getConnection();

            try {

                
                connection.Open();
                String cmdString = "Select * From [dbo].[JOB] WHERE userid=" + userId;

                System.Diagnostics.Debug.Write("Trebuie sa dau joburile pt");
                SqlCommand cmd = new SqlCommand(cmdString, connection);
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
                           Convert.ToInt32(reader[9]),
                           Convert.ToInt32(reader[10])
                           ));

                    }
                }



            }
            catch {
                //exception handle
            }
            finally { connection.Close(); }



            return jobList;

        }

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

        public static IEnumerable<Job> getJobs() {
            SqlConnection connection = DbManager.getConnection();
            try {
                connection.Close();
            }
            catch { }
            List<Job> jobs = new List<Job>();
            try {


                connection.Open();
                String cmdString = "Select * From [dbo].[JOB] ";

                SqlCommand cmd = new SqlCommand(cmdString, connection);
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


    }
}