using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Application.Managers
{
    public interface IPositionManager
    {
        void SetPosition(int figure, int color, string oldPosition, string newPosition);

    }
}
