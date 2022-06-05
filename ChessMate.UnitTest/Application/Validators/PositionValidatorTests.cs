using ChessMate.Application.Exceptions;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure;
using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using Moq;
using System;
using Xunit;

namespace ChessMate.UnitTest.Application.Validators
{
    public class PositionValidatorTests
    {
        Mock<IRepository<FigureEntity>> _figureRepo;
        Mock<IRepository<ColorEntity>> _colorRepo;
        IPositionValidator _sut;

        public PositionValidatorTests()
        {
            
            _figureRepo = new Mock<IRepository<FigureEntity>>();
            _colorRepo = new Mock<IRepository<ColorEntity>>();
            _sut = new PositionValidator(_figureRepo.Object, _colorRepo.Object);
        }

        [Fact]
        public void Should_Throw_Exception_When_Figure_Is_Incorrect()
        {
            _figureRepo.Setup(x => x.Get(1)).Returns(()=> null);
            _colorRepo.Setup(x => x.Get(2)).Returns(new ColorEntity());
            var exception = Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, 1, "", "");
            });
            Assert.Equal("figureId", exception.PropertyName);
            Assert.Equal("Figure is incorrect", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Color_Is_Incorrect()
        {
            _figureRepo.Setup(x => x.Get(1)).Returns(() => new FigureEntity());
            _colorRepo.Setup(x => x.Get(2)).Returns(() => null);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, 2, "", "");
            });
            Assert.Equal("colorId", exception.PropertyName);
            Assert.Equal("Color is incorrect", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Old_Position_Letter_Incorrect()
        {
            _figureRepo.Setup(x => x.Get(1)).Returns(() => new FigureEntity());
            _colorRepo.Setup(x => x.Get(2)).Returns(new ColorEntity());

            var exception = Assert.Throws<ValidationException>(() =>
            {
                _sut.ValidatePosition(1, 2, "p4", "");
            });
            Assert.Equal("oldPosition", exception.PropertyName);
            Assert.Equal("Old Position is incorrect", exception.Message);
        }
    }
}
