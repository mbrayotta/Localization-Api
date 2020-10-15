using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodeController : ControllerBase
    {
        private readonly IGeocodedAddressRepository _repository;

        public GeocodeController(IGeocodedAddressRepository repository)
        {
            _repository = repository;
        }

        // GET api/Geocode/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {         
            var response = await _repository.GetById(id);
            
            return Ok(response);
        }
    }
}