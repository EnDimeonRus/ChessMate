using ChessMate.Application.Exceptions;
using ChessMate.Application.Managers;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure.Entities;
using ChessMate.Infrastructure.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChessMate.UnitTest.Application.Managers
{
    public class PositionManagerTests
    {
        Mock<IPositionValidator> _positionValidator;
        Mock<IRepository<PositionEntity>> _positionRepository;
        IPositionManager _sut;

        public PositionManagerTests()
        {
            _positionValidator = new Mock<IPositionValidator>();
            _positionRepository = new Mock<IRepository<PositionEntity>>();
            _sut = new PositionManager(_positionValidator.Object, _positionRepository.Object);
        }


        [Fact]
        public async Task Should_Save_New_Position()
        {
            await _sut.SetPositionAsync(1, 1, "a1", "b1");

            _positionValidator.Verify(x => x.ValidatePositionAsync(1, 1, "a1", "b1"), Times.Once);
            _positionRepository.Verify(x => x.CreateAsync(It.IsAny<PositionEntity>()), Times.Once);
        }

        [Fact]
        public async Task Should_Receive_Validation_Exception_On_Incorrect_Inputs()
        {
            _positionValidator.Setup(x => x.ValidatePositionAsync(1, 1, "a1", "a1"))
                .Throws(new ValidationException(It.IsAny<string>(), It.IsAny<string>()));

            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.SetPositionAsync(1, 1, "a1", "a1"));
            _positionValidator.Verify(x => x.ValidatePositionAsync(1, 1, "a1", "a1"), Times.Once);
            _positionRepository.Verify(x => x.CreateAsync(It.IsAny<PositionEntity>()), Times.Never);
        }

        [Fact]
        public void Should_Return_Position_On_Request()
        {
            PositionEntity entity = new PositionEntity()
            {
                Color = new ColorEntity()
                {
                    Description = "Цвет"
                },
                Figure = new FigureEntity()
                {
                    Description = "Ферзь",
                    Code = "Ф"
                },
                CurrentPosition = "a1",
                PreviousPosition = "a2",
                InsertDate = System.DateTimeOffset.UtcNow,
                FigureID = 1,
                ColorID = 1
            };
            List<PositionEntity> lst = new List<PositionEntity>();
            lst.Add(entity);


            _positionRepository.SetupGet(x => x.Table).Returns(lst.AsQueryable());
            _positionValidator.Setup(x => x.ValidatePositionAsync(1, 1, "", ""))
                .Throws(new ValidationException(It.IsAny<string>(), It.IsAny<string>()));

            var result =  _sut.GetPosition(1, 1);

            Assert.Equal(entity.Color.Description, result.Color);
            Assert.Equal(entity.Figure.Description, result.Figure);
            Assert.Equal(entity.PreviousPosition, result.PreviousPosition);
            Assert.Equal(entity.CurrentPosition, result.CurrentPosition);
        }

        [Fact]
        public void Should_Return_Last_Position_OnRequest()
        {
            var time = System.DateTimeOffset.UtcNow;
            var timeInPast = time.AddMinutes(-1);

            PositionEntity entityInPast = new PositionEntity()
            {
                Color = new ColorEntity()
                {
                    Description = "Цвет"
                },
                Figure = new FigureEntity()
                {
                    Description = "Ферзь",
                    Code = "Ф"
                },
                CurrentPosition = "a1",
                PreviousPosition = "a2",
                InsertDate = timeInPast,
                FigureID = 1,
                ColorID = 1
            };

            PositionEntity entityCurrent = new PositionEntity()
            {
                Color = new ColorEntity()
                {
                    Description = "Цвет"
                },
                Figure = new FigureEntity()
                {
                    Description = "Ферзь",
                    Code = "Ф"
                },
                CurrentPosition = "a3",
                PreviousPosition = "a4",
                InsertDate = time,
                FigureID = 1,
                ColorID = 1
            };
            var lst = new List<PositionEntity>
            {
                entityInPast,
                entityCurrent
            };


            _positionRepository.SetupGet(x => x.Table).Returns(lst.AsQueryable());
           
            var result = _sut.GetPosition(1, 1);

            Assert.Equal(entityCurrent.Color.Description, result.Color);
            Assert.Equal(entityCurrent.Figure.Description, result.Figure);
            Assert.Equal(entityCurrent.PreviousPosition, result.PreviousPosition);
            Assert.Equal(entityCurrent.CurrentPosition, result.CurrentPosition);
        }
    }
}
