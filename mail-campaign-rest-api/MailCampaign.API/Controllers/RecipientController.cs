using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailCampaign.Api.Controllers
{
    [Route("recipient")]
    [ApiController]
    public class RecipientController : ControllerBase
    {

        private readonly IRecipientService _recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            _recipientService = recipientService ?? throw new ArgumentNullException(nameof(recipientService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetAllRecipient()
        {
            var response = await _recipientService.GetAllRecipient().ConfigureAwait(false);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}
