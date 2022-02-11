using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Services
{
    public class UserEmailTemplateService : IUserEmailTemplateService
    {
        public readonly IUserEmailTemplateRepository _userEmailTemplateRepository;

        public UserEmailTemplateService(IUserEmailTemplateRepository userEmailTemplateRepository)
        {
            _userEmailTemplateRepository = userEmailTemplateRepository ?? throw new ArgumentNullException(nameof(userEmailTemplateRepository));
        }

        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllUserEmailTemplate()
        {
            return await _userEmailTemplateRepository.GetAllUserEmailTemplate();
        }

        public async Task<UserEmailTemplateModal> GetUserEmailTemplateById(int id)
        {
            return await _userEmailTemplateRepository.GetUserEmailTemplateById(id);
        }

        public async Task<bool> Create(UserEmailTemplateModal fields)
        {
            return await _userEmailTemplateRepository.Create(fields);
        }

        public async Task<bool> Update(int id, UserEmailTemplateModal fields)
        {
            return await _userEmailTemplateRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userEmailTemplateRepository.Delete(id);
        }
    }
}
