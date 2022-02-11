using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Core.Services
{
    public class TemplateService : ITemplateService
    {
        public readonly ITemplateRepository _templateRepository;

        public TemplateService(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
        }

        public async Task<IEnumerable<TemplateModal>> GetAllTemplate()
        {
            return await _templateRepository.GetAllTemplate();
        }

        public async Task<TemplateModal> GetTemplateById(int id)
        {
            return await _templateRepository.GetTemplateById(id);
        }

        public async Task<bool> Create(TemplateModal fields)
        {
            return await _templateRepository.Create(fields);
        }

        public async Task<bool> Update(int id, TemplateModal fields)
        {
            return await _templateRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _templateRepository.Delete(id);
        }
    }
}
