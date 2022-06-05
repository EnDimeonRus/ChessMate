using ChessMate.Application.Validators;

namespace ChessMate.Application.Managers
{
    public class PositionManager : IPositionManager
    {
        IPositionValidator _positionValidator;

        public PositionManager(IPositionValidator positionValidator)
        {
            _positionValidator = positionValidator;
        }

        public void SetPosition(int figure, int color, string oldPosition, string newPosition)
        {
            _positionValidator.ValidatePositionAsync(figure, color, oldPosition, newPosition);
        }
    }
}