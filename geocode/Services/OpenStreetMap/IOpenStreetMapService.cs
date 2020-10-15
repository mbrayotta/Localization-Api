using Domain;
using System.Threading.Tasks;

namespace Services.OpenStreetMap
{
    public interface IOpenStreetMapService
    {
        Task ConsumerOpenStreetMap(Address address);
    }
}