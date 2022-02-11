using MailCampaign.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<TemplateModal>> GetAllTemplate();
        Task<TemplateModal> GetTemplateById(int id);
        Task<bool> Create(TemplateModal fields);
        Task<bool> Update(int id, TemplateModal fields);
        Task<bool> Delete(int id);
    }
}
