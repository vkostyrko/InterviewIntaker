namespace InterviewIntaker.Infrastructure.ServiceBus
{
    /// <summary>
    /// Service bus handler interface
    /// </summary>
    public interface IServiceBusHandler
    {
        /// <summary>
        /// Sends service bus message
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="payload">Payload</param>
        /// <param name="queueName">Queue name</param>
        /// <returns></returns>
        Task SendMessageAsync<T>(T payload, string queueName);

        /// <summary>
        /// Receives service bus message
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="onMessageReceived">On message received function</param>
        /// <param name="queueName">Queue name</param>
        /// <returns></returns>
        Task ReceiveMessagesAsync<T>(Func<T, Task> onMessageReceived, string queueName);
    }
}
