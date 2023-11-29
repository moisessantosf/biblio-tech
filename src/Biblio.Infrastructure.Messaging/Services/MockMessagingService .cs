using Biblio.Infrastructure.Messaging.Interfaces;

namespace Biblio.Infrastructure.Messaging.Services
{
    public class MockMessagingService : IMessagingService
    {
        public Task SendMessageAsync<T>(T message)
        {
            if (message is LoanMessage loanMessage)
            {
                Console.WriteLine($"Loan registered: Book {loanMessage.BookId}, Customer {loanMessage.CustomerId}");
            }
            else if (message is ReturnMessage returnMessage)
            {
                Console.WriteLine($"Return registered: Book {returnMessage.BookId}, Customer {returnMessage.CustomerId}");
            }
            else
            {
                Console.WriteLine($"Unknown message registered: {message}");
            }

            return Task.CompletedTask;
        }
    }
}
