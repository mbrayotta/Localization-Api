using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public interface IGeocodedAddressRepository
    {
         Task<GeocodedAddress> GetById(int id);
    }
}