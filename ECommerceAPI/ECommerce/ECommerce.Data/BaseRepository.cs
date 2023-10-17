using ECommerce.Model.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace ECommerce.Data
{
    public class BaseRepository
    {
        #region Fields
        public readonly IConfiguration configuration;
        public readonly IOptions<DataConfig> ConnectionString;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration , IOptions<DataConfig> connectionString)
        {
            this.configuration = configuration;
        }
        #endregion

        #region SQL Methods
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string connString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string conString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            using (SqlConnection _db = new SqlConnection(conString))
            {
                await _db.OpenAsync();
                return await _db.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string connString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string connString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (SqlConnection con = new SqlConnection(connString))
            {
                await con.OpenAsync();
                return await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

    }
}
