using Domain;

namespace Services.RabbitMq.Sender
{
    public interface ISendGeocodedAddress
    {
         void SendGeocodedAddres(GeocodedAddress gaddress);
    }
}