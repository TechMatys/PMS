using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Api.Controllers
{
    [Route("user-email-template")]
    [ApiController]
    public class UserEmailTemplateController : ControllerBase
    {

        private readonly IUserEmailTemplateService _userEmaiTemplateGroupService;

        public UserEmailTemplateController(IUserEmailTemplateService userEmaiTemplateGroupService)
        {
            _userEmaiTemplateGroupService = userEmaiTemplateGroupService ?? throw new ArgumentNullException(nameof(userEmaiTemplateGroupService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEmailTemplateModal>>> GetAllUserEmailTemplate()
        {
            var response = await _userEmaiTemplateGroupService.GetAllUserEmailTemplate().ConfigureAwait(false);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEmailTemplateModal>> GetUserEmailTemplateById(int id)
        {
            return await _userEmaiTemplateGroupService.GetUserEmailTemplateById(id).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] UserEmailTemplateModal entity)
        {
            return await _userEmaiTemplateGroupService.Create(entity);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UserEmailTemplateModal fields)
        {
            return await _userEmaiTemplateGroupService.Update(id, fields);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _userEmaiTemplateGroupService.Delete(id);
        }
    }
}
