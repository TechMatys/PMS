using System;
using System.Collections.Generic;
using System.Text;

namespace MailCampaign.Core.Models
{

    public class Recipient
    {
        public int RecipientId { get; set; }
        public string EmailAddress { get; set; }
        public string AliasName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
