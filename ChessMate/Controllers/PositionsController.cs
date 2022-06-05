using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
