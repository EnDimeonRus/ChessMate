﻿using ChessMate.Application.Exceptions;
using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using System;
using System.Linq;

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
        char[] _possibleLetters = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g','h' };
        char[] _possibleDigits = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

        public PositionValidator(IRepository<FigureEntity> figureRepository, IRepository<ColorEntity> colorRepository)
        {
            _figureRepository = figureRepository;
            _colorRepository = colorRepository;
        }

        public void ValidatePosition(int figureId, int colorId, string oldPosition, string newPosition)
        {
            ValidateFigure(figureId);

            ValidateColor(colorId);

            ValidatePosition(oldPosition,nameof(oldPosition), ERROR_TEXT_OLD_POSITION);

            ValidatePosition(newPosition, nameof(newPosition), ERROR_TEXT_NEW_POSITION);

        }

        private void ValidatePosition(string position, string errorField, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Length != 2)
            {
                throw new ValidationException(errorField, errorMessage);
            }

            if (!_possibleLetters.Contains(position[0]))
            {
                throw new ValidationException(errorField, errorMessage);
            }

            if (!_possibleDigits.Contains(position[1]))
            {
                throw new ValidationException(errorField, errorMessage);
            }


        }

        private void ValidateColor(int colorId)
        {
            var color = _colorRepository.Get(colorId);
            if (color == null)
            {
                throw new ValidationException(nameof(colorId), ERRPR_TEXT_COLOR);
            }
        }

        private void ValidateFigure(int figureId)
        {
            var figure = _figureRepository.Get(figureId);
            if (figure == null)
            {
                throw new ValidationException(nameof(figureId), ERROR_TEXT_FIGURE);
            }
        }
    }
}