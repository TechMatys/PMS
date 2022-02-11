using MailCampaign.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Services
{
    public interface IUserEmailTemplateService
    {
        Task<IEnumerable<UserEmailTemplateModal>> GetAllUserEmailTemplate();
        Task<UserEmailTemplateModal> GetUserEmailTemplateById(int id);
        Task<bool> Create(UserEmailTemplateModal fields);
        Task<bool> Update(int id, UserEmailTemplateModal fields);
        Task<bool> Delete(int id);
    }
}
