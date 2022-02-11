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

        public Task<IEnumerable<UserEmailTemplateModal>> GetAllUserEmailTemplate()
        {
            throw new NotImplementedException();
        }

        public Task<UserEmailTemplateModal> GetUserEmailTemplateById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(UserEmailTemplateModal fields)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, UserEmailTemplateModal fields)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
