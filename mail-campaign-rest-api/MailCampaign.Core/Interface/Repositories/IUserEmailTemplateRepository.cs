using MailCampaign.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Repositories
{
    public interface IUserEmailTemplateRepository
    {
        Task<IEnumerable<UserEmailTemplateModal>> GetAllSentMail();
        Task<IEnumerable<UserEmailTemplateModal>> GetAllDraftMail();
        Task<bool> SendMail(UserEmailTemplateModal fields);
    }
}
