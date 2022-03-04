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

        public Task<bool> SendMail(UserEmailTemplateModal fields)
        {
            try
            {
                var query = @"INSERT INTO UserEmailTemplates(Subject, HtmlContent, ScheduleDate, StatusId, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@Subject, @HtmlContent, @ScheduleDate, @StatusId, 1, -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.Subject,
                        fields.HtmlContent,
                        fields.StatusId,
                        ScheduleDate = fields.StatusId == 2 ? fields.ScheduleDate : null
                    });

                    return Task.FromResult(true);
                }
            }
            catch (Exception exp)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllSentMail()
        {
            try
            {
                var query = @"SELECT UserEmailTemplateId
                                    ,Subject
                                    ,HtmlContent    
	                                ,Convert(varchar(10), CreatedDate, 110) as SentDate
                                FROM UserEmailTemplates where IsDeleted = 0 and StatusId = 1 order by CreatedDate desc";

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

        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllDraftMail()
        {
            try
            {
                var query = @"SELECT UserEmailTemplateId
                                    ,Subject
                                    ,HtmlContent    
	                                ,Convert(varchar(10), CreatedDate, 110) as DraftDate
                                FROM UserEmailTemplates where IsDeleted = 0 and StatusId = 2";

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
    }
}
