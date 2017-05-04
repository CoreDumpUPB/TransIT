using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {

    public static class ClientManager {

        public static Object insertClient(Client client) {
            if (client == null)
                return "Wrong parameters";
            SqlConnection connection = DbManager.getConnection();
            try {
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

        public static Client getClientbyEmail(String email) {
            SqlConnection connection = DbManager.getConnection();
            try {


                connection.Open();
                String cmdString = "Select * From [dbo].[User] Where email='" + email + "'";

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                Client client = null;
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {

                        int clientId = Convert.ToInt32(reader[0]);
                        client = new Client(clientId, reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), JobManager.GetJobsByUserId(clientId));
                    }
                }

                connection.Close();
                return client;
            }
            catch { connection.Close(); return null; }

        }

        public static Client getClientbyEmail(String email, String password) {
            SqlConnection connection = DbManager.getConnection();
            try {


                connection.Open();
                String cmdString = "Select * From [dbo].[User] Where email='" + email + "' AND password='" + password + "'";

                SqlCommand cmd = new SqlCommand(cmdString, connection);
                Client client = null;
                using (SqlDataReader reader = cmd.ExecuteReader()) {

                    if (reader.Read()) {

                        int clientId = Convert.ToInt32(reader[0]);
                        client = new Client(clientId, reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), JobManager.GetJobsByUserId(clientId));
                    }
                }

                connection.Close();
                return client;
            }
            catch { connection.Close(); return null; }

        }

        public static Client getClientbyID(int ID) {
            SqlConnection connection = DbManager.getConnection();

            Client client = null;
            try {
                connection.Open();
                String cmdString = "Select * From [User] Where Id=" + ID;

                SqlCommand cmd = new SqlCommand(cmdString, connection);

        
                
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        int clientId = Convert.ToInt32(reader[0]);
                        System.Diagnostics.Debug.Write(String.Format("request for cliendID: {0}", reader[0] ));
                        client = new Client(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), JobManager.GetJobsByUserId(clientId));
                    }
                }

                connection.Close();
                return client;
            }
            catch {
                return null;
            }

            return client;
        }
    }
}