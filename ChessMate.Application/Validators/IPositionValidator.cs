using System.Threading.Tasks;

namespace ChessMate.Application.Validators
{
    public interface IPositionValidator
    {
        public Task ValidatePositionAsync(int figureId, int colorId, string oldPosition, string newPosition);
    }
}
