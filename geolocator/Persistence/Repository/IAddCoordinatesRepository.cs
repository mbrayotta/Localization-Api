using System.Threading.Tasks;
using Domain;

namespace Persistence.Repository
{
    public interface IAddCoordinatesRepository
    {
        Task InsertCoordinates(GeocodedAddress geocodedAddress);
    }
}