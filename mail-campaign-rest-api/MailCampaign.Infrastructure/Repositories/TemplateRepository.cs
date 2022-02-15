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
    public class TemplateRepository : ITemplateRepository
    {

        private readonly IConfiguration configuration;

        public TemplateRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<TemplateModal>> GetAllTemplate()
        {
            try
            {
                var query = @"SELECT TemplateId 
                                    ,Title
	                                ,IsActive
	                                ,CreatedBy
	                                ,Convert(nvarchar(10),CreatedDate,110) as CreatedDate
                                FROM Templates where IsDeleted = 0";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<TemplateModal>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<TemplateModal> GetTemplateById(int id)
        {
            try
            {
                var query = @"SELECT Title
	                                ,Description
                                    ,HtmlContent
                              FROM Templates where TemplateId = @TemplateId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    return (await connection.QueryAsync<TemplateModal>(query, new
                    {
                        TemplateId = id

                    })).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public Task<bool> Create(TemplateModal fields)
        {
            try
            {
                var query = @"INSERT INTO Templates(Title, Description, HtmlContent, IsActive, CreatedBy, CreatedDate) 
                              VALUES (@Title, @Description, @HtmlContent, 1, -1, GetUtcDate())";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new {
                        fields.Title,
                        fields.Description,
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

        public Task<bool> Update(int id, TemplateModal fields)
        {
            try
            {
                var query = @"UPDATE Templates
                                SET Title = @Title
	                                ,Description = @Description
                                    ,HtmlContent = @HtmlContent
	                                ,ModifiedBy = -1
	                                ,ModifiedDate = GetUtcDate()
                                WHERE TemplateId = @TemplateId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        fields.Title,
                        fields.Description,
                        fields.HtmlContent,
                        TemplateId = id
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
                var query = @"UPDATE Templates
                                SET IsActive = 0
                                    ,IsDeleted = 1
	                                ,DeletedBy = -1
	                                ,DeletedDate = GetUtcDate()
                                WHERE TemplateId = @TemplateId";

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Execute(query, new
                    {
                        TemplateId = id
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
