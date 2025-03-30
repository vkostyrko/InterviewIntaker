using AutoMapper;
using InterviewIntaker.Application.Task;
using InterviewIntaker.Contracts.Tasks;
using InterviewIntaker.Data.Repositories;
using Moq;

namespace InterviewIntaker.Application.Tests;

/// <summary>
/// <see cref="CreateTaskCommandHandler"/> tests
/// </summary>
[TestClass]
public class CreateTaskCommandHandlerTests
{
    [TestMethod]
    public async System.Threading.Tasks.Task Handle_MapsAndCreatesTask_ReturnsCreatedTask()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        var inputTaskModel = new CreateTaskRequest() { Name = "New Task" };
        var mappedTask = new InterviewIntaker.Domain.DomainModels.Task() { Name = "New Task" };
        var createdTask = new InterviewIntaker.Domain.DomainModels.Task{ Id = Guid.NewGuid(), Name = "New Task" };

        var command = new CreateTaskCommand(inputTaskModel);

        mockMapper.Setup(m => m.Map<InterviewIntaker.Domain.DomainModels.Task>(inputTaskModel)).Returns(mappedTask);
        mockRepository.Setup(r => r.Create(mappedTask)).ReturnsAsync(createdTask);

        var handler = new CreateTaskCommandHandler(mockRepository.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(createdTask.Id, result.Id);
        Assert.AreEqual(createdTask.Name, result.Name);

        mockMapper.Verify(m => m.Map<InterviewIntaker.Domain.DomainModels.Task>(inputTaskModel), Times.Once);
        mockRepository.Verify(r => r.Create(mappedTask), Times.Once);
    }
}