using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {

    public static class ClientManager {
        public static String connectionString = DbManager.connectionString;

        public static int insertClient(Client client) {

             using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();

                try {
          
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

                    SqlCommand cmd = new SqlCommand(sb.ToString(), con);

                    client.clientID = Convert.ToInt32(cmd.ExecuteScalar());
                    return client.clientID;


                }

                catch(Exception exp) {
                    throw exp;
                }

            }

        }

        public static Client getClientbyEmail(String email) {


            using (SqlConnection con = new SqlConnection(connectionString)) {

                if (con.State == ConnectionState.Closed) con.Open();

                    try {


                        
                        String cmdString = "Select * From [dbo].[User] Where email='" + email + "'";

                        SqlCommand cmd = new SqlCommand(cmdString, con);
                        Client client = null;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {

                                
                                client = new Client(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                        
                        }
                        }

                        
                        return client;
                    }
                    catch {
                        return null;
                    }
                }
            }
           

        

        public static Client CheckClient(string email, string password) {

   
            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();

                try {

                    String cmdString = "SELECT * From [dbo].[User] Where email='" + email + "' AND password='" + password + "'";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    System.Diagnostics.Debug.Write(cmdString);
                    Client clientEdited = null;

                    System.Diagnostics.Debug.Write("Verific client!");
                    using (SqlDataReader reader = cmd.ExecuteReader()) {

                        if (reader.Read()) {

                            
                            clientEdited = new Client(Convert.ToInt32(reader[0]),reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
             
                        }
                    }


                    return clientEdited;




                }
                catch(Exception ex) {
                    throw ex;
                }

            }

         
        }


 
        public static Client getClientbyID(int ID) {
            //SqlConnection connection = DbManager.getConnection();

            Client client = null;

            using (SqlConnection con = new SqlConnection(connectionString)) {
                if (con.State == ConnectionState.Closed) con.Open();
                try {
                    
                    String cmdString = "Select * From [User] Where Id=" + ID;

                    SqlCommand cmd = new SqlCommand(cmdString, con);



                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {

                            
                            client = new Client(ID,reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                   

                        }
                    }

                   
                  
                }
                catch {}


            }

            return client;
        }
    }
}