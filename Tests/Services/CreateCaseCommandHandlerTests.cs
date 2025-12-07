using ApplicationLayer.Features.Cases.Commands.CreatCases;
using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using FluentAssertions;
using Moq;

namespace Tests.Services;

public class CreateCaseCommandHandlerTests
{
    private Mock<ICaseRepository> _caseRepositoryMock;
    private Mock<ICurrentUserService> _currentUserServiceMock;
    private Mock<IMapper> _mapperMock;

    private CreateCaseCommandHandler _handler;
    [SetUp]
    public void Setup()
    {
        _caseRepositoryMock = new Mock<ICaseRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateCaseCommandHandler(
            _caseRepositoryMock.Object,
            _mapperMock.Object,
            _currentUserServiceMock.Object);

        _handler = new CreateCaseCommandHandler(
            _caseRepositoryMock.Object,
            _mapperMock.Object,
            _currentUserServiceMock.Object);
    }

    [Test]
    public async Task Handle_Should_ReturnFailure_whenUserIsNotLoggedIn()
    {
        //Arrange
        _currentUserServiceMock.Setup(x => x.UserId).Returns(0);

        var command = new CreateCaseCommand("Test Case", "Description", 1, 2, []);
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Unauthorized: user must be logged in.");
    }

    [Test]

    public async Task Handle_Should_CreateCaseSuccessfully_whenDataIsValid()
    {
        //Arrange
        _currentUserServiceMock.Setup(x => x.UserId).Returns(5);
        var command = new CreateCaseCommand("Test Case", "Description", 1, 2, [1, 2]);
        var createdCase = new Case
        {
            Id = 1,
            Title = command.Title,
            Description = command.Description,
            ClientId = command.ClientId

        };

        _caseRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Case>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(DomainLayer.Common.OperationResult<Case>.Success(createdCase));

        _mapperMock.Setup(x => x.Map<CaseDto>(It.IsAny<Case>()))
            .Returns(new CaseDto { Id = 1, Title = "Test Case" });

        //Act

        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Data.Id.Should().Be(1);
        result.Data.Title.Should().Be("Test Case");




    }
}