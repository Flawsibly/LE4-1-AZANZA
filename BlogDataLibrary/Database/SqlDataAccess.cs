using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private const string connectionStringName = "SqlDb";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionStringName)))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionStringName)))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
