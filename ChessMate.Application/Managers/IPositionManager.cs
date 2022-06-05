using ChessMate.Models.Models;
using System.Threading.Tasks;

namespace ChessMate.Application.Managers
{
    public interface IPositionManager
    {
        Task SetPositionAsync(int figureId, int colorId, string oldPosition, string newPosition);

        Position GetPosition(int figureId, int colorId);
    }
}
