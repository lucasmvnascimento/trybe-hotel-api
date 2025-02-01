using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult GetCities(){
            return Ok(_repository.GetCities());
        }

        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            return StatusCode(StatusCodes.Status201Created, _repository.AddCity(city));
        }

        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            try
            {
                var updatedCity = _repository.UpdateCity(city);
                return Ok(updatedCity);
            }
            catch (InvalidDataException e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}