using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailCampaign.Infrastructure.Entities
{
    [Table("Recipients")]
    public class Recipient
    {
        [Key]
        public int RecipientId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
