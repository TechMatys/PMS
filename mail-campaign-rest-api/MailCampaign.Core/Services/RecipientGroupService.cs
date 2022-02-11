using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Services
{
    public class RecipientGroupService : IRecipientGroupService
    {
        public readonly IRecipientGroupRepository _recipientGroupRepository;

        public RecipientGroupService(IRecipientGroupRepository recipientGroupRepository)
        {
            _recipientGroupRepository = recipientGroupRepository ?? throw new ArgumentNullException(nameof(recipientGroupRepository));
        }

        public async Task<IEnumerable<RecipientGroupModal>> GetAllRecipientGroup()
        {
            return await _recipientGroupRepository.GetAllRecipientGroup();
        }

        public async Task<RecipientGroupModal> GetRecipientGroupById(int id)
        {
            return await _recipientGroupRepository.GetRecipientGroupById(id);
        }

        public async Task<bool> Create(RecipientGroupModal fields)
        {
            return await _recipientGroupRepository.Create(fields);
        }

        public async Task<bool> Update(int id, RecipientGroupModal fields)
        {
            return await _recipientGroupRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _recipientGroupRepository.Delete(id);
        }
    }
}
