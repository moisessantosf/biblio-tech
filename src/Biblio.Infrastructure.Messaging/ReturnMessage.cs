namespace Biblio.Infrastructure.Messaging
{
    public class ReturnMessage
    {
            public int CustomerId { get; set; }
            public int BookId { get; set; }
            public DateTime? ReturnDate { get; set; }
    }
}
