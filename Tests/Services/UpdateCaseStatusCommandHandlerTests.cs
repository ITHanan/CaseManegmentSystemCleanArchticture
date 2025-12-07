
using ApplicationLayer.Features.ChangeCaseStatus.Commands;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using FluentAssertions;
using Moq;

namespace Tests.Services;

    public class UpdateCaseStatusCommandHandlerTests
    {
        private Mock<ICaseRepository> _caseRepoMock;
        private Mock<ICurrentUserService> _currentUserMock;
        private UpdateCaseStatusCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _caseRepoMock = new Mock<ICaseRepository>();
            _currentUserMock = new Mock<ICurrentUserService>();

            _handler = new UpdateCaseStatusCommandHandler(
                _caseRepoMock.Object,
                _currentUserMock.Object
            );
        }

     
        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserNotLoggedIn()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(0);

            var command = new UpdateCaseStatusCommand(1, CaseStatus.InProgress);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Unauthorized user.");
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCaseNotFound()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(10);

            _caseRepoMock
                .Setup(r => r.GetCaseWithDetailsAsync(1))
                .ReturnsAsync((Case?)null);

            var command = new UpdateCaseStatusCommand(1, CaseStatus.Open);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Case not found.");
        }

    
        [Test]
        public async Task Handle_ShouldFail_WhenNonAdminTriesToCloseCase()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(10);
            _currentUserMock.Setup(x => x.Role).Returns("User");

            var caseObj = new Case { Id = 1, AssignedToUserId = 10, Status = CaseStatus.InProgress };

            _caseRepoMock
                .Setup(r => r.GetCaseWithDetailsAsync(1))
                .ReturnsAsync(caseObj);

            var command = new UpdateCaseStatusCommand(1, CaseStatus.Closed);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Only Administrator can close cases.");
        }

     
        [Test]
        public async Task Handle_ShouldFail_WhenNonAssignedUserStartsCase()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(5);
            _currentUserMock.Setup(x => x.Role).Returns("User");

            var caseObj = new Case { Id = 1, AssignedToUserId = 99 };

            _caseRepoMock
                .Setup(r => r.GetCaseWithDetailsAsync(1))
                .ReturnsAsync(caseObj);

            var command = new UpdateCaseStatusCommand(1, CaseStatus.InProgress);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Only assigned user or admin can start working on this case.");
        }

        [Test]
        public async Task Handle_ShouldSuccess_WhenAdminClosesCase()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(1);
            _currentUserMock.Setup(x => x.Role).Returns("Admin");

            var caseObj = new Case { Id = 1, AssignedToUserId = 1 };

            _caseRepoMock
                .Setup(r => r.GetCaseWithDetailsAsync(1))
                .ReturnsAsync(caseObj);

            _caseRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Case>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(OperationResult<Case>.Success(caseObj));

            var command = new UpdateCaseStatusCommand(1, CaseStatus.Closed);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

       
        [Test]
        public async Task Handle_ShouldSuccess_WhenAssignedUserStartsCase()
        {
            // Arrange
            _currentUserMock.Setup(x => x.UserId).Returns(5);
            _currentUserMock.Setup(x => x.Role).Returns("User");

            var caseObj = new Case { Id = 1, AssignedToUserId = 5 };

            _caseRepoMock
                .Setup(r => r.GetCaseWithDetailsAsync(1))
                .ReturnsAsync(caseObj);

            _caseRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Case>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(OperationResult<Case>.Success(caseObj));

            var command = new UpdateCaseStatusCommand(1, CaseStatus.InProgress);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }

