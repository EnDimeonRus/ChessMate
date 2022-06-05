using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        IRepository<ColorEntity> _colorRep;
        public PositionsController(IRepository<ColorEntity> colorRep)
        {
            _colorRep = colorRep;
        }

        // GET api/<PositionsController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var color = await _colorRep.GetAsync(1);
            await _colorRep.CreateAsync(new ColorEntity()
            {
                Description = "Серый"
            });
            return "value";
        }

        // POST api/<PositionsController>
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
        }


    }
}
