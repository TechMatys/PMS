using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Api.Controllers
{
    [Route("template")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateGroupService;

        public TemplateController(ITemplateService templateGroupService)
        {
            _templateGroupService = templateGroupService ?? throw new ArgumentNullException(nameof(templateGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateModal>>> GetAllTemplate()
        {
            var response = await _templateGroupService.GetAllTemplate();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateModal>> GetTemplateById(int id)
        {
            var response = await _templateGroupService.GetTemplateById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] TemplateModal templateModal)
        {
            return await _templateGroupService.Create(templateModal);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] TemplateModal templateModal)
        {
            return await _templateGroupService.Update(id, templateModal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _templateGroupService.Delete(id);
        }
    }
}
