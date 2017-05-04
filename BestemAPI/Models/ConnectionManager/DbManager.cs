using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {
    public static class DbManager {


        private static SqlConnection connection = null;

        private static String connectionString = "Server=tcp:transapp.database.windows.net,1433;Initial Catalog=trans-db;Persist Security Info=False;User ID=transapp-admin;Password=Coredump123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection getConnection() {

            if (connection == null) {
                try {
                    connection = new SqlConnection(connectionString);
                }
                catch {

                    return null;
                }
            }

            return connection;
        }
    }
}