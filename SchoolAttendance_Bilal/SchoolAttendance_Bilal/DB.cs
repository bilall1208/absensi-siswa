using MySql.Data.MySqlClient;
using System.Data;

namespace T1_Bilal_XIRPLA
{
    class DB
    {
        public static MySqlConnection koneksi = new MySqlConnection("server = 127.0.0.1; username = root; password = ; database = school_attendance");
        public static DataSet ds = new DataSet();
        public static MySqlDataAdapter da;
        public static MySqlCommand perintah;

        public static void crud(string query)
        {
            ds.Tables.Clear();
            perintah = new MySqlCommand(query, koneksi);
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds);
        }
    }
}
