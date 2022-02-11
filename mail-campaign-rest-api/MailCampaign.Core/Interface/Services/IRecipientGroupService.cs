using MailCampaign.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Services
{
    public interface IRecipientGroupService
    {
        Task<IEnumerable<RecipientGroupModal>> GetAllRecipientGroup();
        Task<RecipientGroupModal> GetRecipientGroupById(int id);
        Task<bool> Create(RecipientGroupModal fields);
        Task<bool> Update(int id, RecipientGroupModal fields);
        Task<bool> Delete(int id);
    }
}
