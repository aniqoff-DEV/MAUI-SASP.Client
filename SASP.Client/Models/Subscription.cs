namespace SASP.Client.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        public int IssueId { get; set; }

        public int UserId { get; set; }

        public DateTime StartSub { get; set; }

        public DateTime EndSub { get; set; }

        public decimal Price { get; set; }
    }
}
