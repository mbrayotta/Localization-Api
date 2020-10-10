using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocateController : ControllerBase
    {
        private readonly AddressRepository _repository;

        public GeolocateController(AddressRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task Post([FromBody] Address address)
        {
            await _repository.Insert(address);
        }

    }
}