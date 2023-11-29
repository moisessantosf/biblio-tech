namespace Biblio.Infrastructure.Messaging
{
    public class LoanMessage
    {
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
