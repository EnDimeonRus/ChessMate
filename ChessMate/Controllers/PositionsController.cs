using ChessMate.Application.Exceptions;
using ChessMate.Application.Managers;
using ChessMate.Models.Models;
using ChessMate.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace ChessMate.API.Controllers
{
    /// <summary>
    /// Work with figure position
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {

        private IPositionManager _positionManager;
        private ILogger<PositionsController> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="positionManager">POsition manager</param>
        public PositionsController(IPositionManager positionManager, ILogger<PositionsController> logger)
        {
            _positionManager = positionManager;
            _logger = logger;
            _logger.LogInformation("Position controller initialized");
        }

        /// <summary>
        /// Get current position for figure with specific color
        /// </summary>
        /// <param name="figure">Фигура 1-Пешка, 2-Слон, 3-Конь, 4-Ладья, 5-Ферзь, 6-Король</param>
        /// <param name="color">Цвет фигуры 1-Белый, 2-Черный</param>
        /// <returns></returns>
        [HttpGet("{figure}/{color}")]
        [ProducesResponseType(typeof(PositionViewModel),200)]
        public IActionResult Get(int figure, int color)
        {
            try
            {
                var figurePosition = _positionManager.GetPosition(figure, color);
                if(figurePosition == null)
                {
                    return Ok("Position is not exist");
                }
                return Ok(figurePosition);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something bad happened {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Store position for specific figure
        /// </summary>
        /// <param name="position">Information about figure and position</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "saveFigurePosition")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post([FromBody] Position position)
        {
            try
            {
                await _positionManager.SetPositionAsync(position.Figure, position.Color, position.PreviousPosition, position.CurrentPosition);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation error field {ex.PropertyName} error {ex.Message}");
                return ValidationProblem(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something bad happened {ex.Message}");
                return BadRequest();

            }
            return Ok();
        }




    }
}
