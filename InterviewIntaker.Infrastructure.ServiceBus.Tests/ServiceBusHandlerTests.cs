using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Moq;

namespace InterviewIntaker.Infrastructure.ServiceBus.Tests;

/// <summary>
/// <see cref="ServiceBusHandler"/> tests
/// </summary>
[TestClass]
public class ServiceBusHandlerTests
{
    private Mock<ServiceBusClient> mockClient;
    private Mock<ServiceBusSender> mockSender;
    private ServiceBusHandler handler;

    [TestInitialize]
    public void Setup()
    {
        var options = Options.Create(new ServiceBusOptions
        {
            ConnectionString = "Endpoint=sb://test.local/;SharedAccessKeyName=fake;SharedAccessKey=fake"
        });

        mockClient = new Mock<ServiceBusClient>();
        mockSender = new Mock<ServiceBusSender>();

        mockClient
            .Setup(client => client.CreateSender(It.IsAny<string>()))
            .Returns(mockSender.Object);

        
        handler = new TestableHandler(mockClient.Object, options);
    }

    [TestMethod]
    public async Task SendMessageAsync_SendsCorrectSerializedMessage()
    {
        // Arrange
        var payload = new TestPayload { Id = 42, Name = "Hello" };

        // Act
        await handler.SendMessageAsync(payload, "my-queue");

        // Assert
        mockSender.Verify(sender =>
                sender.SendMessageAsync(
                    It.IsAny<ServiceBusMessage>(),
                    CancellationToken.None),
            Times.Once);
    }

    [TestMethod]
    public async Task SendMessageAsync_HandlesSendFailureGracefully()
    {
        // Arrange
        mockSender
            .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default))
            .ThrowsAsync(new Exception("Boom"));

        var payload = new TestPayload { Id = 1, Name = "Test" };

        // Act & Assert
        await handler.SendMessageAsync(payload, "fail-queue");

        
        mockSender.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default), Times.Exactly(4));
    }

    public class TestPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private class TestableHandler : ServiceBusHandler
    {
        private readonly ServiceBusClient _mockClient;

        public TestableHandler(ServiceBusClient client, IOptions<ServiceBusOptions> options)
            : base(options)
        {
            _mockClient = client;
        }

        protected override ServiceBusSender CreateSender(string queueName)
        {
            return _mockClient.CreateSender(queueName);
        }
    }
}