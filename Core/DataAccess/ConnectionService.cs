using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace Core.DataAccess
{
    public static class ConnectionService
    {
        public static string connectionString = "";

        public static void SetNpgsql(IConfiguration configuration, string connectionStringName)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
            {
                ConnectionString = configuration.GetConnectionString(connectionStringName),
                Host = configuration["Npgsql:Host"],
                Database = configuration["Npgsql:Database"],
                Port = Convert.ToInt32(configuration["Npgsql:Port"]),
                Username = configuration["Npgsql:Username"],
                Password = configuration["Npgsql:Password"]
            };
            connectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
