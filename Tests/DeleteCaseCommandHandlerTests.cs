using ApplicationLayer.Features.Cases.Commands.DeleteCase;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using FluentAssertions;
using InfrastructureLayer.Repositories;
using Moq;

namespace Tests;

    public class DeleteCaseCommandHandlerTests
    {
        private Mock<IGenericRepository<Case>> _caseRepoMock;
        private DeleteCaseCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _caseRepoMock = new Mock<IGenericRepository<Case>>();
            _handler = new DeleteCaseCommandHandler(_caseRepoMock.Object);
        }

  
        
        [Test]
        public async Task Handle_ShouldReturnFailure_WhenRepositoryReturnsFailure()
        {
        // Arrange
        _caseRepoMock
            .Setup(r => r.DeleteByIdAsync(1))
                .ReturnsAsync(OperationResult<bool>.Failure("Case not found."));

            var command = new DeleteCaseCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Case not found.");
        }


   
        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            _caseRepoMock
                .Setup(r => r.DeleteByIdAsync(1))
                .ReturnsAsync(OperationResult<bool>.Success(true));

            var command = new DeleteCaseCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeTrue();
        }
}

