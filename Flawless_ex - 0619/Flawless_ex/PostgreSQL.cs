using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Flawless_ex
{
    class PostgreSQL
    {
        public NpgsqlConnection connection()
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;";
            //conn.ConnectionString = @"Server = 192.168.152.164; Port = 5432; User Id = postgres; Password = postgres; Database = master;";
            //conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;";
            return conn;
        }
    }
}
