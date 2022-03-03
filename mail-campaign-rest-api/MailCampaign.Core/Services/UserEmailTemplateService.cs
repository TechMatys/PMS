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

        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllSentMail()
        {
            return await _userEmailTemplateRepository.GetAllSentMail();
        }

        public async Task<IEnumerable<UserEmailTemplateModal>> GetAllDraftMail()
        {
            return await _userEmailTemplateRepository.GetAllDraftMail();
        }

        public async Task<bool> SendMail(UserEmailTemplateModal fields)
        {
            return await _userEmailTemplateRepository.SendMail(fields);
        }

    }
}
