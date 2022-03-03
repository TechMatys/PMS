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

        [HttpPost("send-mail")]
        public async Task<ActionResult<bool>> SendMail([FromBody] UserEmailTemplateModal entity)
        {
            entity.StatusId = 1;
            return Ok(await _userEmaiTemplateGroupService.SendMail(entity));
        }

        [HttpPost("draft-mail")]
        public async Task<ActionResult<bool>> DraftMail([FromBody] UserEmailTemplateModal entity)
        {
            entity.StatusId = 2;
            return Ok(await _userEmaiTemplateGroupService.SendMail(entity));
        }

        [HttpPost("send-later")]
        public async Task<ActionResult<bool>> SendLaterMail([FromBody] UserEmailTemplateModal entity)
        {
            entity.StatusId = 3;
            return Ok(await _userEmaiTemplateGroupService.SendMail(entity));

        }
        [HttpGet("sent-mail")]
        public async Task<ActionResult<IEnumerable<UserEmailTemplateModal>>> GetAllSentMail()
        {
            return Ok(await _userEmaiTemplateGroupService.GetAllSentMail());
        }

        [HttpGet("draft-mail")]
        public async Task<ActionResult<IEnumerable<UserEmailTemplateModal>>> GetAllDraftMail()
        {
            return Ok(await _userEmaiTemplateGroupService.GetAllDraftMail());
        }


    }
}
