using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebService
{
    public static class DBController
    {
        public static IDbConnection GetDBConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
        }
            

    }
}