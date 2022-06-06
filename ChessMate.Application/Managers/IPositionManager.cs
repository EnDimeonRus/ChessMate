using ChessMate.Models.ViewModels;
using System.Threading.Tasks;

namespace ChessMate.Application.Managers
{
    public interface IPositionManager
    {
        Task SetPositionAsync(int figureId, int colorId, string oldPosition, string newPosition);

        PositionViewModel GetPosition(int figureId, int colorId);
    }
}
