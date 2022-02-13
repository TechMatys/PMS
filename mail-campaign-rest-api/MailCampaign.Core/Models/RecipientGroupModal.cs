using System;
using System.Collections.Generic;
using System.Text;

namespace MailCampaign.Core.Models
{
    public class RecipientGroupModal
    {
        public int RecipientGroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string EmailAddresses { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public List<Recipient> RecipientList { get; set; }
    }
}
