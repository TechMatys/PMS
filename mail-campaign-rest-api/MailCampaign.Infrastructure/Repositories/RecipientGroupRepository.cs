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
    public class RecipientGroupRepository : IRecipientGroupRepository
    {
        private readonly IConfiguration configuration;

        public RecipientGroupRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<RecipientGroupModal>> GetAllRecipientGroup()
        {
            try
            {
                var query = @"SELECT RecipientGroupId
                                    ,GroupName
	                                ,CreatedBy
	                                ,Convert(varchar(10), CreatedDate, 110) as CreatedDate
                                FROM RecipientGroups where IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<RecipientGroupModal>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<RecipientGroupModal> GetRecipientGroupById(int id)
        {
            try
            {
                var query = @"SELECT GroupName
	                                ,Description
	                                ,'' as EmailAddresses
                                FROM RecipientGroups
                                WHERE RecipientGroupId = @RecipientGroupId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<RecipientGroupModal>(query, new
                    {
                        RecipientGroupId = id
                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(RecipientGroupModal fields)
        {
            try
            {
                var query = @"INSERT INTO RecipientGroups(GroupName, Description, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@GroupName, @Description, 1, -1, GetUtcDate()) ";

                //query = query + @"Insert into Recipients(EmailAddress, IsActive, CreatedBy, CreatedDate)
                //                values(@EmailAddress, 1, -1, GetUtcDate()) ";

                //query = query + @"Insert into RecipientGroupMapping(RecipientId, RecipientGroupId, IsActive, CreatedBy, CreatedDate)
                //                values(@RecipientId, @RecipientGroupId, 1, -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.GroupName,
                        fields.Description
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, RecipientGroupModal fields)
        {
            try
            {
                var query = @"UPDATE RecipientGroups
                                SET GroupName = @GroupName
	                                ,Description = @Description
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE RecipientGroupId = @RecipientGroupId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.GroupName,
                        fields.Description,
                        RecipientGroupId = id
                    });

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
                var query = @"UPDATE RecipientGroups
                                SET IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE RecipientGroupId = @RecipientGroupId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        RecipientGroupId = id
                    });

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
