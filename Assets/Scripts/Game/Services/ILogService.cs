namespace Game.Services
{
    /// <summary>
    /// Provides logging functionalities.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);
    }
}