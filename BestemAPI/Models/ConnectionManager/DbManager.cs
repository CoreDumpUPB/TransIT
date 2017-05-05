using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BestemAPI.Models.ConnectionManager {
    public static class DbManager {




        public static String connectionString = "Server=tcp:transapp.database.windows.net,1433;Initial Catalog=trans-db;Persist Security Info=False;User ID=transapp-admin;Password=Coredump123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

       
    }
}