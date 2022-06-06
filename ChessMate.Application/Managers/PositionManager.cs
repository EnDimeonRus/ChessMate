using ChessMate.Application.Validators;
using ChessMate.Infrastructure.Entities;
using ChessMate.Infrastructure.Repository;
using ChessMate.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMate.Application.Managers
{
    public class PositionManager : IPositionManager
    {
        IPositionValidator _positionValidator;
        IRepository<PositionEntity> _positionRepository;

        public PositionManager(IPositionValidator positionValidator, IRepository<PositionEntity> positionRepository)
        {
            _positionValidator = positionValidator;
            _positionRepository = positionRepository;
        }

        public async Task SetPositionAsync(int figureId, int colorId, string oldPosition, string newPosition)
        {
            await _positionValidator.ValidatePositionAsync(figureId, colorId, oldPosition, newPosition);
            var entity = new PositionEntity()
            {
                ColorID = colorId,
                FigureID = figureId,
                PreviousPosition = oldPosition,
                CurrentPosition = newPosition,
                InsertDate = System.DateTimeOffset.UtcNow
            };
            await _positionRepository.CreateAsync(entity);
        }

        public PositionViewModel GetPosition(int figureId, int colorId)
        {
            var positionEntity = _positionRepository.Table
                        .Include(x => x.Figure)
                        .Include(x => x.Color)
                        .Where(x => x.FigureID == figureId && x.ColorID == colorId)
                        .OrderByDescending(x => x.InsertDate).FirstOrDefault();
            if (positionEntity != null)
            {
                return new PositionViewModel()
                {
                    Figure = positionEntity.Figure.Description,
                    Color = positionEntity.Color.Description,
                    CurrentPosition = positionEntity.CurrentPosition,
                    PreviousPosition = positionEntity.PreviousPosition
                };
            }
            return null;
        }

    }
}