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
    public class UserEmailTemplateRepository : IUserEmailTemplateRepository
    {

        private readonly IConfiguration configuration;

        public UserEmailTemplateRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllUserEmailTemplate()
        {
            try
            {
                var query = @"SELECT UserEmailTemplateId 
                                    ,Subject
	                                ,IsActive
	                                ,CreatedBy
	                                ,Convert(nvarchar(10),CreatedDate,110) as CreatedDate
                                FROM UserEmailTemplates where IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<UserEmailTemplateModal>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<UserEmailTemplateModal> GetUserEmailTemplateById(int id)
        {
            try
            {
                var query = @"SELECT Subject
                                    ,HtmlContent
                              FROM UserEmailTemplates where UserEmailTemplateId = @UserEmailTemplateId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<UserEmailTemplateModal>(query, new
                    {
                        UserEmailTemplateId = id

                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(UserEmailTemplateModal fields)
        {
            try
            {
                var query = @"INSERT INTO UserEmailTemplates(Subject, HtmlContent, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@Subject, @HtmlContent, 1, -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.Subject,
                        fields.HtmlContent
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
                var query = @"UPDATE UserEmailTemplates
                                SET IsActive = 0
                                    ,IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE UserEmailTemplateId = @UserEmailTemplateId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        UserEmailTemplateId = id
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(int id, UserEmailTemplateModal fields)
        {
            throw new NotImplementedException();
        }
    }
}
