using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace InterviewIntaker.Infrastructure.ServiceBus;

/// <summary>
/// Implements <see cref="IServiceBusHandler"/>
/// </summary>
public class ServiceBusHandler : IServiceBusHandler
{
    protected readonly ServiceBusClient client;
    private readonly AsyncRetryPolicy _retryPolicy;

    public ServiceBusHandler(IOptions<ServiceBusOptions> options)
    {
        client = new ServiceBusClient(options.Value.ConnectionString);

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i));
    }

    public async Task SendMessageAsync<T>(T payload, string queueName)
    {
        var sender = CreateSender(queueName);
        var json = JsonSerializer.Serialize(payload);
        var message = new ServiceBusMessage(json);

        try
        {
            await _retryPolicy.ExecuteAsync(() => sender.SendMessageAsync(message));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Send ERROR {ex.Message}");
        }
    }

    public async Task ReceiveMessagesAsync<T>(Func<T, Task> onMessageReceived, string queueName)
    {
        var processor = CreateProcessor(queueName);

        processor.ProcessMessageAsync += async args =>
        {
            try
            {
                var body = args.Message.Body.ToString();
                var obj = JsonSerializer.Deserialize<T>(body);
                if (obj != null)
                {
                    await onMessageReceived(obj);
                }

                await args.CompleteMessageAsync(args.Message);
            }
            catch
            {
                await args.AbandonMessageAsync(args.Message);
            }
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Processor ERROR {args.Exception.Message}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }

    protected virtual ServiceBusSender CreateSender(string queueName) =>
        client.CreateSender(queueName);

    protected virtual ServiceBusProcessor CreateProcessor(string queueName) =>
        client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
}