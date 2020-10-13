using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public interface IAddressRepository
    {
        Task Insert(Address address);
    }
}