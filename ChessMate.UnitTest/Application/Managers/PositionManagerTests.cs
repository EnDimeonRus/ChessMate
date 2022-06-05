using ChessMate.Application.Exceptions;
using ChessMate.Application.Managers;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure.Entities;
using ChessMate.Infrastructure.Repository;
using Moq;
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
            await _sut.SetPositionAsync(1, 1, "", "");

            _positionValidator.Verify(x => x.ValidatePositionAsync(1, 1, "", ""), Times.Once);
            _positionRepository.Verify(x => x.CreateAsync(It.IsAny<PositionEntity>()), Times.Once);
        }

        [Fact]
        public async Task Should_Receive_Validation_Exception_On_Incorrect_Inputs()
        {
            _positionValidator.Setup(x => x.ValidatePositionAsync(1, 1, "", ""))
                .Throws(new ValidationException(It.IsAny<string>(), It.IsAny<string>()));


            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.SetPositionAsync(1, 1, "", ""));
            _positionValidator.Verify(x => x.ValidatePositionAsync(1, 1, "", ""), Times.Once);
            _positionRepository.Verify(x => x.CreateAsync(It.IsAny<PositionEntity>()), Times.Never);
        }
    }
}
