using Domain;

namespace Services.RabbitMq.Sender
{
    public interface ISendAddress
    {
         void SendAddressToGetCoordinates(Address address);
    }
}