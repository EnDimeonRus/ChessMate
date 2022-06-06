using ChessMate.Application.Exceptions;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure.Entities;
using ChessMate.Infrastructure.Repository;
using Moq;
using System.Threading.Tasks;
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
        public async Task Should_Throw_Exception_When_Figure_Is_Incorrect()
        {
            var figureId = 1;
            _figureRepo.Setup(x => x.GetAsync(figureId)).ReturnsAsync(() => null);
            _colorRepo.Setup(x => x.GetAsync(2)).ReturnsAsync(new ColorEntity());

            await ValidateFigure(figureId);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Color_Is_Incorrect()
        {
            int colorId = 2;
            _figureRepo.Setup(x => x.GetAsync(1)).ReturnsAsync(new FigureEntity());
            _colorRepo.Setup(x => x.GetAsync(colorId)).ReturnsAsync(() => null);

           await ValidateColor(colorId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("alotofletters")]
        [InlineData("p4")]
        public async Task  Should_Throw_Exception_When_Old_Position_Letter_Is_Incorrect(string oldPosition)
        {
            SetupCorrectFigureAndColor();

            await ValidateOldPosition(oldPosition);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("alotofletters")]
        [InlineData("p4")]
        public async Task Should_Throw_Exception_When_New_Position_Letter_Is_Incorrect(string newPosition)
        {
            SetupCorrectFigureAndColor();

            await ValidateNewPosition(newPosition);
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("A1")]
        [InlineData("b1")]
        [InlineData("c1")]
        [InlineData("d1")]
        [InlineData("e1")]
        [InlineData("f1")]
        [InlineData("g1")]
        [InlineData("h1")]
        public async void Should_Not_Throw_Exception_When_New_Positions_Letter_Correct(string position)
        {
            SetupCorrectFigureAndColor();

            await _sut.ValidatePositionAsync(1, 2, "b3", position);
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("A1")]
        [InlineData("b1")]
        [InlineData("c1")]
        [InlineData("d1")]
        [InlineData("e1")]
        [InlineData("f1")]
        [InlineData("g1")]
        [InlineData("h1")]
        public async void Should_Not_Throw_Exception_When_Old_Positions_Letter_Correct(string position)
        {
            SetupCorrectFigureAndColor();

            await _sut.ValidatePositionAsync(1, 2, position, "b3");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Old_Position_Digit_Is_Incorrect()
        {
            SetupCorrectFigureAndColor();

            await ValidateOldPosition("a0");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_New_Position_Digit_Is_Incorrect()
        {
            SetupCorrectFigureAndColor();

            await ValidateNewPosition("a0");
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("A1")]
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

            _sut.ValidatePositionAsync(1, 2, position, position);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_New_Position_Is_The_Same_As_Old_Position()
        {
            SetupCorrectFigureAndColor();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.ValidatePositionAsync(1, 2, "a1", "a1");
            });

            Assert.Equal("newPosition", exception.PropertyName);
            Assert.Equal("Positions can not be the same", exception.Message);
        }
        #region Validate Calls

        private async Task ValidateFigure(int figureId)
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.ValidatePositionAsync(figureId, 1, PROPER_POSITION, PROPER_POSITION);
            });

            Assert.Equal("figureId", exception.PropertyName);
            Assert.Equal("Figure is incorrect", exception.Message);
        }

        private async Task ValidateColor(int colorId)
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.ValidatePositionAsync(1, colorId, PROPER_POSITION, PROPER_POSITION);
            });

            Assert.Equal("colorId", exception.PropertyName);
            Assert.Equal("Color is incorrect", exception.Message);
        }

        private async Task ValidateOldPosition(string oldPosition)
        {
            var exception = await  Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.ValidatePositionAsync(1, 2, oldPosition, PROPER_POSITION);
            });

            Assert.Equal("oldPosition", exception.PropertyName);
            Assert.Equal("Old Position is incorrect", exception.Message);
        }

        private async Task ValidateNewPosition(string newPosition)
        {
            var exception =await  Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.ValidatePositionAsync(1, 2, PROPER_POSITION, newPosition);
            });

            Assert.Equal("newPosition", exception.PropertyName);
            Assert.Equal("New Position is incorrect", exception.Message);
        }
        #endregion

        private void SetupCorrectFigureAndColor()
        {
            _figureRepo.Setup(x => x.GetAsync(1)).ReturnsAsync(new FigureEntity());
            _colorRepo.Setup(x => x.GetAsync(2)).ReturnsAsync(new ColorEntity());
        }
    }
}
