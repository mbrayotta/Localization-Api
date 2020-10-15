using Domain;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Services.RabbitMq.Sender;

namespace Services.OpenStreetMap
{
    public class OpenStreetMapService : IOpenStreetMapService
    {
        private readonly ISendGeocodedAddress _sendGeocodedAddress;

        public OpenStreetMapService(ISendGeocodedAddress sendGeocodedAddressnder)
        {
            _sendGeocodedAddress = sendGeocodedAddressnder;
        }
        public async Task ConsumerOpenStreetMap(Address address)
        {
            using (var httpClient = new HttpClient())
            {
                var content = $"https://nominatim.openstreetmap.org/?addressdetails=1&q={address.Numero},{address.Calle.Replace(' ','+')},{address.Provincia.Replace(' ','+')},{address.Ciudad.Replace(' ','+')},{address.CodigoPostal},{address.Pais.Replace(' ','+')}&format=json&limit=1";
                using (var response = await httpClient.GetAsync(content))
                {
                    System.Console.WriteLine(response);
                    /*
                        obtener latitude y longitud desde response
                        crear objeto GeocodedAddress
                        pasarlo a _sendGeocodedAddress()
                    */
                }
            }
        }
    }
}