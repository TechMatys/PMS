using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace MailCampaign.Infrastructure.Repositories
{
    public class ConnectionRepository
    {
        private readonly IConfiguration _config;
        private readonly IWebHost _env;

        public ConnectionRepository(IConfiguration config, IWebHost env)
        {
            _config = config;
            _env = env;
        }

        public IDbConnection CreateConnection()
        {
            string databaseName = _config["dbName"]; ;
            string connectionString = _config.GetConnectionString("DefaultConnection");
            string password = _config["dbPassword"];

            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder(connectionString)
            {
                Password = password,
                InitialCatalog = databaseName
            };


            return new SqlConnection(conn.ConnectionString);
        }
    }
}
