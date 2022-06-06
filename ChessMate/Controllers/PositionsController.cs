using ChessMate.Application.Managers;
using ChessMate.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ChessMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {

        public IPositionManager _positionManager;

        public PositionsController(IPositionManager positionManager)
        {
            _positionManager = positionManager;
        }

        /// <summary>
        /// Store position for specific figure
        /// </summary>
        /// <param name="position">Information about figure and position</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "saveFigurePosition")]
        public async Task<IActionResult> Post([FromBody] Position position)
        {
            await _positionManager.SetPositionAsync(int.Parse(position.Figure), int.Parse(position.Color), position.PreviousPosition, position.CurrentPosition);
            return Ok();
        }

        /// <summary>
        /// Get current position for figure with specific color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="figure"></param>
        /// <returns></returns>
        [HttpGet("{figure}/{color}")]
        public Position Get(int figure, int color)
        {
            return  _positionManager.GetPosition(figure, color);
        }

        


    }
}
