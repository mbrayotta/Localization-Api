using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services.RabbitMq.Sender;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocateController : ControllerBase
    {
        private readonly IAddressRepository _repository;
        private readonly ISendAddress _sendAddress;

        public GeolocateController(IAddressRepository repository, ISendAddress sendAddress)
        {
            _repository = repository;
            _sendAddress = sendAddress;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Address address)
        {
            IActionResult result = BadRequest();
            
            try{
                
                int code = await _repository.Insert(address);
                
                if(code > 0){
                    result = Accepted(code);
                    _sendAddress.SendAddressToGetCoordinates(address);
                }
            
            }catch(Exception ex){
                result = BadRequest(ex.Message);
            }
            

            return result;
        }

    }
}