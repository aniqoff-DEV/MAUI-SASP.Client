namespace SASP.Client.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int IssueId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }
    }
}
