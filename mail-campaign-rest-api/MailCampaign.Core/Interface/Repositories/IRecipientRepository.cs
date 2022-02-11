using MailCampaign.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Interface.Repositories
{
    public interface IRecipientRepository 
    {
        Task<IEnumerable<Recipient>> GetAllRecipient();
        Task<Recipient> GetRecipientById(int id);
        Task<bool> Create(Recipient fields);
        Task<bool> Update(int id, Recipient fields);
        Task<bool> Delete(int id);
    }
}
