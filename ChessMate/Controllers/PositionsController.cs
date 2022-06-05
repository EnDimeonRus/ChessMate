using ChessMate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        // GET api/<PositionsController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {

            return "value";
        }

        // POST api/<PositionsController>
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
        }


    }
}
