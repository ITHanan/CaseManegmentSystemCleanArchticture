using ApplicationLayer.Features.Cases.Commands.UpdateCase;
using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using FluentAssertions;
using Moq;

namespace Tests;

public class UpdateCaseCommandHandlerTests
{

    private Mock<ICaseRepository> _caseRepoMack;
    private Mock<ICurrentUserService> _currentUserMock;
    private Mock<IMapper> _MapperMack;

    private UpdateCaseCommandHandler _handler;
    [SetUp]
    public void Setup()
    {
        _caseRepoMack = new Mock<ICaseRepository>();
        _currentUserMock = new Mock<ICurrentUserService>();
        _MapperMack = new Mock<IMapper>();

        _handler = new UpdateCaseCommandHandler(
            _caseRepoMack.Object,
            _MapperMack.Object,
            _currentUserMock.Object);
    }

    [Test]
    public async Task Handle_shouldReturnFailure_whenUserNotLoggedIn()
    {
        //Arrange
        _currentUserMock.Setup(c => c.UserId).Returns(0);

        var command = new UpdateCaseCommand(
            1, // Id
            "Updated Title", // Title
            "Updated Description", // Description
            2, // AssignedToUserId
            new List<int> { 1, 2 }, // TagIds
            CaseStatus.Closed // Status
        );


        //Act

        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("Unauthorized");
    }

    [Test]
    public async Task Handle_ShouldReturnFailure_WhenCaseDoesNotExist()
    {
        //Arrange
        _currentUserMock.Setup(x => x.UserId).Returns(5);

        _caseRepoMack
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(OperationResult<Case>.Failure("Not Found"));

        var command = new UpdateCaseCommand
        (
             1,
            "Test",
            "Desc",
             2,
             new List<int> { },
             CaseStatus.Open
        );
        
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("Case not found");
    }
    
    [Test]
    public async Task Handle_ShouldReturnSuccess_WhenCaseIsUpdated()
    {
        // Arrange
        _currentUserMock.Setup(x => x.UserId).Returns(5);
        _currentUserMock.Setup(x => x.Role).Returns("Admin");

        var existingCase = new Case
        {
            Id = 1,
            Title = "Old Title",
            Description = "Old Desc",
            AssignedToUserId = 3,
            CreatedByUserId = 5,
            Status = CaseStatus.Open,
            CaseTags = new List<CaseTag>()
        };

        _caseRepoMack
            .Setup(r => r.GetCaseWithDetailsAsync(1))
            .ReturnsAsync(existingCase);

        _caseRepoMack.Setup(r => r.UpdateAsync(It.IsAny<Case>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Case c, CancellationToken ct) => OperationResult<Case>.Success(c));


        _MapperMack.Setup(m => m.Map<CaseDto>(It.IsAny<Case>()))
            .Returns((Case c) => new CaseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                AssignedToUserId = c.AssignedToUserId,
                Status = c.Status
            });


        var command = new UpdateCaseCommand(
             1,
            "New Title",
            "New Description",
            4,
            new List<int>(),
            CaseStatus.Closed
            );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // ASSERT
        result.IsSuccess.Should().BeTrue();
        result.Data!.Title.Should().Be("New Title");
        result.Data.Description.Should().Be("New Description");
        result.Data.AssignedToUserId.Should().Be(4);
        result.Data.Status.Should().Be(CaseStatus.Closed);
    }



}
