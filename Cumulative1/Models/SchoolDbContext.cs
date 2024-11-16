﻿using MySql.Data.MySqlClient;

namespace Cumulative1.Models
{
    public class SchoolDbContext
    {


        //Read only properties
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "cumulative1_http5125"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                   + "; user = " + User
                   + "; database = " + Database
                   + "; port = " + Port
                   + "; password = " + Password
                   + ";convert zero datetime = True";
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);

        }
    }
}