using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Application.Validators
{
    public interface IPositionValidator
    {
        public void ValidatePosition(int figureId, int colorId, string oldPosition, string newPosition);
    }
}
