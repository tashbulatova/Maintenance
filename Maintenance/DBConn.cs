using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Maintenance
{
    class DBConn
    {
        public static string connstring = String.Format("Server = {0}; Port = {1};" + "User Id = {2}; Password = {3}; Database = {4};", "localhost", "5432", "postgres", "123", "maintenance");
        public static NpgsqlConnection db = new NpgsqlConnection(connstring);
        
    }
}
