using Dapper;
using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
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
                var query = @"SELECT RecipientGroupId
	                            ,GroupName
	                            ,Description
	                            ,(
		                            SELECT rc.RecipientId as recipientId
			                            ,rc.EmailAddress as emailAddress
		                            FROM RecipientGroupMapping rgm
		                            INNER JOIN Recipients rc ON rc.RecipientId = rgm.RecipientId
		                            WHERE rgm.RecipientGroupId = @RecipientGroupId and rgm.IsDeleted = 0
		                            FOR JSON AUTO
		                            ) AS RecipientListData
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
                string jsonData = JsonSerializer.Serialize(fields);
                var query = @"INSERT INTO RecipientGroups(GroupName, Description, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@GroupName, @Description, 1, -1, GetUtcDate())                     
                              
                              Declare @RecipientGroupId int = Scope_Identity() 

                              Declare @Temp_Recipient table(RecipientId int, EmailAddress varchar(100)) 
                              
                              Insert into @Temp_Recipient
                              Select RecipientId, EmailAddress FROM OPENJSON(@jsonData, N'$.RecipientList') 
				              WITH (RecipientId int N'$.RecipientId', 
				              EmailAddress varchar(100) N'$.EmailAddress') Recipient 
                              
                              --Insert email into recipients (if not exists)
                              Insert into Recipients(EmailAddress, IsActive, CreatedBy, CreatedDate)
                              Select tr.EmailAddress, 1, -1, GetUtcDate() from @Temp_Recipient tr
                              Left Join Recipients rc on rc.EmailAddress = tr.EmailAddress
                              where IsNull(rc.RecipientId,0) = 0 
                              
                              --Insert into recipients mapping table (if new entry)
                              Insert into RecipientGroupMapping(RecipientId, RecipientGroupId, IsActive, CreatedBy, CreatedDate)
                              Select rc.RecipientId, @RecipientGroupId, 1, -1, GetUtcDate() from @Temp_Recipient tr
                              Inner Join Recipients rc on rc.EmailAddress = tr.EmailAddress
                              Left Join RecipientGroupMapping rgm on rgm.RecipientId = rc.RecipientId and rgm.RecipientGroupId = @RecipientGroupId and rgm.IsDeleted = 0
                              where IsNull(rgm.RecipientGroupMappingId,0) = 0 
                              
                              
                              --Update recipients mapping table records(if existing entry)
                              Update rgm set IsActive = 0, IsDeleted = 1, DeletedBy = -1, DeletedDate = GetUtcDate() from RecipientGroupMapping rgm
                              Inner Join Recipients rc on rc.RecipientId = rgm.RecipientId
                              Left Join @Temp_Recipient tr on tr.EmailAddress = rc.EmailAddress
                              where rgm.RecipientGroupId = @RecipientGroupId and IsNull(tr.EmailAddress,'') = '' and rgm.IsDeleted = 0 ";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    var id = connection.Execute(query, new
                    {
                        fields.GroupName,
                        fields.Description,
                        jsonData
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
                string jsonData = JsonSerializer.Serialize(fields);

                var query = @"UPDATE RecipientGroups
                                SET GroupName = @GroupName
	                                ,Description = @Description
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE RecipientGroupId = @RecipientGroupId 

                            Declare @Temp_Recipient table(RecipientId int, EmailAddress varchar(100)) 

                            Insert into @Temp_Recipient
                            Select RecipientId, EmailAddress FROM OPENJSON(@jsonData, N'$.RecipientList') 
				            WITH (RecipientId int N'$.RecipientId', 
				            EmailAddress varchar(100) N'$.EmailAddress') Recipient 

                            --Insert email into recipients (if not exists)
                            Insert into Recipients(EmailAddress, IsActive, CreatedBy, CreatedDate)
                            Select tr.EmailAddress, 1, -1, GetUtcDate() from @Temp_Recipient tr
                            Left Join Recipients rc on rc.EmailAddress = tr.EmailAddress
                            where IsNull(rc.RecipientId,0) = 0 

                            --Insert into recipients mapping table (if new entry)
                            Insert into RecipientGroupMapping(RecipientId, RecipientGroupId, IsActive, CreatedBy, CreatedDate)
                            Select rc.RecipientId, @RecipientGroupId, 1, -1, GetUtcDate() from @Temp_Recipient tr
                            Inner Join Recipients rc on rc.EmailAddress = tr.EmailAddress
                            Left Join RecipientGroupMapping rgm on rgm.RecipientId = rc.RecipientId and rgm.RecipientGroupId = @RecipientGroupId and rgm.IsDeleted = 0
                            where IsNull(rgm.RecipientGroupMappingId,0) = 0 


                            --Update recipients mapping table records(if existing entry)
                            Update rgm set IsActive = 0, IsDeleted = 1, DeletedBy = -1, DeletedDate = GetUtcDate() from RecipientGroupMapping rgm
                            Inner Join Recipients rc on rc.RecipientId = rgm.RecipientId
                            Left Join @Temp_Recipient tr on tr.EmailAddress = rc.EmailAddress
                            where rgm.RecipientGroupId = @RecipientGroupId and IsNull(tr.EmailAddress,'') = '' and rgm.IsDeleted = 0 ";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.GroupName,
                        fields.Description,
                        RecipientGroupId = id,
                        jsonData
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
                                SET IsActive = 0
                                    ,IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE RecipientGroupId = @RecipientGroupId 
                            
                              UPDATE RecipientGroupMapping
                                SET IsActive = 0
	                                ,IsDeleted = 1
	                                ,DeletedBy = - 1
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
