using ChessMate.Application.Exceptions;
using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Application.Validators
{
    public class PositionValidator : IPositionValidator
    {
        IRepository<FigureEntity> _figureRepository;
        IRepository<ColorEntity> _colorRepository;

        private const string IncorrectFigureErrorText = "Figure is incorrect";
        private const string IncorrectColorErrorText = "Color is incorrect";

        public PositionValidator(IRepository<FigureEntity> figureRepository, IRepository<ColorEntity> colorRepository)
        {
            _figureRepository = figureRepository;
            _colorRepository = colorRepository;
        }

        public void ValidatePosition(int figureId, int colorId, string oldPosition, string newPosition)
        {
            var figure = _figureRepository.Get(figureId);
            if(figure == null)
            {
                throw new ValidationException(nameof(figureId), IncorrectFigureErrorText);
            }

            var color = _colorRepository.Get(colorId);
            if (color == null)
            {
                throw new ValidationException(nameof(colorId), IncorrectColorErrorText);
            }
        }
    }
}
