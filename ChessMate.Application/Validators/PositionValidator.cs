using ChessMate.Application.Exceptions;
using ChessMate.Infrastructure.Entities;
using ChessMate.Infrastructure.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMate.Application.Validators
{
    public class PositionValidator : IPositionValidator
    {
        IRepository<FigureEntity> _figureRepository;
        IRepository<ColorEntity> _colorRepository;

        private const string ERROR_TEXT_FIGURE = "Figure is incorrect";
        private const string ERRPR_TEXT_COLOR = "Color is incorrect";
        private const string ERROR_TEXT_OLD_POSITION = "Old Position is incorrect";
        private const string ERROR_TEXT_NEW_POSITION = "New Position is incorrect";
        private const string ERROR_TEXT_SAME_POSITION = "Positions can not be the same";
        char[] _possibleLetters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g','h' };
        char[] _possibleDigits = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

        public PositionValidator(IRepository<FigureEntity> figureRepository, IRepository<ColorEntity> colorRepository)
        {
            _figureRepository = figureRepository;
            _colorRepository = colorRepository;
        }

        public async Task ValidatePositionAsync(int figureId, int colorId, string oldPosition, string newPosition)
        {
            await ValidateFigure(figureId);

            await ValidateColorAsync(colorId);

            ValidatePosition(oldPosition, nameof(oldPosition), ERROR_TEXT_OLD_POSITION);

            ValidatePosition(newPosition, nameof(newPosition), ERROR_TEXT_NEW_POSITION);

            NewMethod(oldPosition, newPosition);
        }

        private static void NewMethod(string oldPosition, string newPosition)
        {
            if (oldPosition == newPosition)
            {
                throw new ValidationException(nameof(newPosition), ERROR_TEXT_SAME_POSITION);
            }
        }

        private void ValidatePosition(string position, string errorField, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Length != 2)
            {
                throw new ValidationException(errorField, errorMessage);
            }

            var positionElements = position.ToLower().ToCharArray();

            if (!_possibleLetters.Contains(positionElements[0]))
            {
                throw new ValidationException(errorField, errorMessage);
            }

            if (!_possibleDigits.Contains(positionElements[1]))
            {
                throw new ValidationException(errorField, errorMessage);
            }
        }

        private async Task ValidateColorAsync(int colorId)
        {
            var color = await _colorRepository.GetAsync(colorId);
            if (color == null)
            {
                throw new ValidationException(nameof(colorId), ERRPR_TEXT_COLOR);
            }
        }

        private async Task ValidateFigure(int figureId)
        {
            var figure = await  _figureRepository.GetAsync(figureId);
            if (figure == null)
            {
                throw new ValidationException(nameof(figureId), ERROR_TEXT_FIGURE);
            }
        }
    }
}
