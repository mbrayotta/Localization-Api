using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public interface IAddressRepository
    {
        Task<int> Insert(Address address);
    }
}