using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BPMTaskDispatch.DBManager
{
    public class DB
    {
        public static IDbConnection HRConnection
        {
            get
            {
                var connHRString = ConfigurationManager.ConnectionStrings["HRConnection"].ConnectionString;
                var conn = new SqlConnection(connHRString);
                conn.Open();
                return conn;
            }

        }


        public static IDbConnection BPMConnection
        {
            get
            {
                var connHRString = ConfigurationManager.ConnectionStrings["BPMConnection"].ConnectionString;
                var conn = new SqlConnection(connHRString);
                conn.Open();
                return conn;
            }
        }
    }
}
