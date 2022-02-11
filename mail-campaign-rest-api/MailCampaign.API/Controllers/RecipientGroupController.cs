using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Api.Controllers
{
    [Route("recipient-group")]
    [ApiController]
    public class RecipientGroupController : ControllerBase
    {
        private readonly IRecipientGroupService _recipientGroupService;

        public RecipientGroupController(IRecipientGroupService recipientGroupService)
        {
            _recipientGroupService = recipientGroupService ?? throw new ArgumentNullException(nameof(recipientGroupService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipientGroupModal>>> GetAllRecipientGroup()
        {
            var response = await _recipientGroupService.GetAllRecipientGroup();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipientGroupModal>> GetRecipientGroupById(int id)
        {
            return await _recipientGroupService.GetRecipientGroupById(id);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] RecipientGroupModal entity)
        {
            return await _recipientGroupService.Create(entity);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] RecipientGroupModal fields)
        {
            return await _recipientGroupService.Update(id, fields);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _recipientGroupService.Delete(id);
        }

    }
}
