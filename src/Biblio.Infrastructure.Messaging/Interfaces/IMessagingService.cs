namespace Biblio.Infrastructure.Messaging.Interfaces
{
    public interface IMessagingService
    {
        Task SendMessageAsync<T>(T message);
    }
}