using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public interface IGeocodedAddress
    {
         Task<GeocodedAddress> GetById(int id);
    }
}