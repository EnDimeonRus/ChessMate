using ChessMate.Application.Managers;
using ChessMate.Application.Validators;
using ChessMate.Infrastructure;
using ChessMate.Infrastructure.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ChessMate.UnitTest.Application.Managers
{
    public class PositionManagerTests
    {
        Mock<IPositionValidator> _positionValidator;
        IPositionManager _sut;

        public PositionManagerTests()
        {
            _positionValidator = new Mock<IPositionValidator>();
            _sut = new PositionManager(_positionValidator.Object);
        }


        [Fact]
        public void Should_Call_Validator_On_Parameters()
        {
            _positionValidator.Setup(x => x.ValidatePosition(1, 1, "", ""));

            _sut.SetPosition(1, 1, "", "");

            _positionValidator.Verify(x => x.ValidatePosition(1, 1, "", ""), Times.Once);
        }
    }
}
