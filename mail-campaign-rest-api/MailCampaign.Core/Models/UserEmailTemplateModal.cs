
namespace MailCampaign.Core.Models
{
    public class UserEmailTemplateModal
    {
        public int UserEmailTemplateId { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public bool IsActive { get; set; }
        public int ManagedBy { get; set; }
        public string CreatedBy { get; set; }
        public string SentDate { get; set; }
        public string DraftDate { get; set; }
        public int StatusId { get; set; }
        public string ScheduleDate { get; set; }
    }
}
