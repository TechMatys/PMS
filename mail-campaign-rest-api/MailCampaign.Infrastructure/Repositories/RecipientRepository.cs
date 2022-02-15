using Dapper;
using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MailCampaign.Infrastructure.Repositories
{
    public class RecipientRepository : IRecipientRepository 
    {
        private readonly IConfiguration configuration;
        public RecipientRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Recipient>> GetAllRecipient()
        {
            try
            {
                var query = @"SELECT EmailAddress
	                                ,AliasName
	                                ,IsActive
	                                ,CreatedBy
	                                ,CreatedDate
                                FROM Recipients where IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<Recipient>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Recipient> GetRecipientById(int id)
        {
            try
            {
                var query = @"SELECT EmailAddress
	                                ,AliasName
	                                ,IsActive
                                FROM Recipients
                                WHERE RecipientId = @RecipientId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<Recipient>(query)).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(Recipient fields)
        {
            try
            {
                var query = @"INSERT INTO Recipients(EmailAddress, AliasName, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@EmailAddress, @AliasName, 1, @CreatedBy, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                     connection.Execute(query);

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, Recipient fields)
        {
            try
            {
                var query = @"UPDATE Recipients
                                SET EmailAddress = @EmailAddress
	                                ,AliasName = @AliasName
	                                ,IsActive = @IsActive
	                                ,ModifiedBy = @ModifiedBy
	                                ,ModifiedDate = GetUtcDate()
                                WHERE RecipientId = @RecipientId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query);

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Delete(int id)
        {
            try
            {
                var query = @"UPDATE Recipients
                                SET IsDeleted = 1
	                                ,DeletedBy = @DeletedBy
	                                ,DeletedDate = GetUtcDate()
                                WHERE RecipientId = @RecipientId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query);

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }
    }
}
