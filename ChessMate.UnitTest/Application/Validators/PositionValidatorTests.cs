using ChessMate.Application.Exceptions;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using Moq;
using Xunit;

namespace ChessMate.UnitTest.Application.Validators
{
    public class PositionValidatorTests
    {
        Mock<IRepository<FigureEntity>> _figureRepo;
        Mock<IRepository<ColorEntity>> _colorRepo;
        IPositionValidator _sut;
        private const string PROPER_POSITION = "a1";
        public PositionValidatorTests()
        {
            _figureRepo = new Mock<IRepository<FigureEntity>>();
            _colorRepo = new Mock<IRepository<ColorEntity>>();
            _sut = new PositionValidator(_figureRepo.Object, _colorRepo.Object);
        }

        [Fact]
        public void Should_Throw_Exception_When_Figure_Is_Incorrect()
        {
            var figureId = 1;
            _figureRepo.Setup(x => x.Get(figureId)).Returns(() => null);
            _colorRepo.Setup(x => x.Get(2)).Returns(new ColorEntity());

            var exception = ValidateFigure(figureId);

            Assert.Equal("figureId", exception.PropertyName);
            Assert.Equal("Figure is incorrect", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Color_Is_Incorrect()
        {
            int colorId = 2;
            _figureRepo.Setup(x => x.Get(1)).Returns(new FigureEntity());
            _colorRepo.Setup(x => x.Get(colorId)).Returns(() => null);

            var exception = ValidateColor(colorId);

            Assert.Equal("colorId", exception.PropertyName);
            Assert.Equal("Color is incorrect", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("alotofletters")]
        [InlineData("p4")]
        public void Should_Throw_Exception_When_Old_Position_Letter_Is_Incorrect(string oldPosition)
        {
            SetupCorrectFigureAndColor();

            ValidateOldPosition(oldPosition);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("alotofletters")]
        [InlineData("p4")]
        public void Should_Throw_Exception_When_New_Position_Letter_Is_Incorrect(string newPosition)
        {
            SetupCorrectFigureAndColor();

            ValidateNewPosition(newPosition);
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("b1")]
        [InlineData("c1")]
        [InlineData("d1")]
        [InlineData("e1")]
        [InlineData("f1")]
        [InlineData("g1")]
        [InlineData("h1")]
        public void Should_Not_Throw_Exception_When_Positions_Letter_Correct(string position)
        {
            SetupCorrectFigureAndColor();

            _sut.ValidatePosition(1, 2, position, position);
        }

        [Fact]
        public void Should_Throw_Exception_When_Old_Position_Digit_Is_Incorrect()
        {
            SetupCorrectFigureAndColor();

            ValidateOldPosition("a0");
        }

        [Fact]
        public void Should_Throw_Exception_When_New_Position_Digit_Is_Incorrect()
        {
            SetupCorrectFigureAndColor();

            ValidateNewPosition("a0");
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("b2")]
        [InlineData("c3")]
        [InlineData("d4")]
        [InlineData("e5")]
        [InlineData("f6")]
        [InlineData("g7")]
        [InlineData("h8")]
        public void Should_Not_Throw_Exception_When_Positions_Digit_Correct(string position)
        {
            SetupCorrectFigureAndColor();

            _sut.ValidatePosition(1, 2, position, position);
        }


        #region Validate Calls

        private ValidationException ValidateFigure(int figureId)
        {
            return Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(figureId, 1, PROPER_POSITION, PROPER_POSITION);
            });
        }

        private ValidationException ValidateColor(int colorId)
        {
            return Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, colorId, PROPER_POSITION, PROPER_POSITION);
            });
        }

        private void ValidateOldPosition(string oldPosition)
        {
            var exception = Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, 2, oldPosition, PROPER_POSITION);
            });

            Assert.Equal("oldPosition", exception.PropertyName);
            Assert.Equal("Old Position is incorrect", exception.Message);
        }

        private void ValidateNewPosition(string newPosition)
        {
            var exception = Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, 2, PROPER_POSITION, newPosition);
            });

            Assert.Equal("newPosition", exception.PropertyName);
            Assert.Equal("New Position is incorrect", exception.Message);
        }
        #endregion

        private void SetupCorrectFigureAndColor()
        {
            _figureRepo.Setup(x => x.Get(1)).Returns(new FigureEntity());
            _colorRepo.Setup(x => x.Get(2)).Returns(new ColorEntity());
        }
    }
}
