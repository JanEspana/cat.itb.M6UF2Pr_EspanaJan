using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace cat.itb.M6UF2Pr_EspanaJan.Connection
{
    public class CloudConnection
    {
        private String HOST = "flora.db.elephantsql.com:5432"; // Ubicació de la BD.
        private String DB = "mgzmmbwh"; // Nom de la BD.
        private String USER = "mgzmmbwh";
        private String PASSWORD = "Ycx0FKBYQtUhDpD-glvDEp0m6yxMTKT3";

        // Specify connection options and open an connection
        public NpgsqlConnection conn = null;

        /**
         * Mètode per connectar a la base de dades school
         */
        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}
