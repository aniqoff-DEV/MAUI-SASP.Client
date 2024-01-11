namespace SASP.Client.Models
{
    public class IssueDto
    {
        public int IssueId { get; set; }

        public string Catalog { get; set; }

        public string TypeIssue { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
