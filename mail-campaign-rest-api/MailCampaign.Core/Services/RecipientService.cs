using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Services
{
    public class RecipientService : IRecipientService
    {
        public readonly IRecipientRepository _recipientRepository;

        public RecipientService(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository ?? throw new ArgumentNullException(nameof(recipientRepository));
        } 

        public async Task<IEnumerable<Recipient>> GetAllRecipient()
        {
            return await _recipientRepository.GetAllRecipient();
        }

        public async Task<Recipient> GetRecipientById(int id)
        {
            return await _recipientRepository.GetRecipientById(id);
        }

        public async Task<bool> Create(Recipient fields)
        {
            return await _recipientRepository.Create(fields);
        }

        public async Task<bool> Update(int id, Recipient fields)
        {
            return await _recipientRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _recipientRepository.Delete(id);
        }
    }
}
