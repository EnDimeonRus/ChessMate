namespace ChessMate.Application.Managers
{
    public interface IPositionManager
    {
        void SetPosition(int figure, int color, string oldPosition, string newPosition);

    }
}
