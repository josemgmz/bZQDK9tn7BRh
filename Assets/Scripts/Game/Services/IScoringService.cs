
using Game.Services.Lifetime;

namespace Game.Services
{
    /// <summary>
    /// Provides functionalities to manage scoring and combo in the game.
    /// </summary>
    public interface IScoringService : IStartableService, IStoppableService
    {
        /// <summary>
        /// Resets the scoring for the specified round.
        /// </summary>
        /// <param name="currentRound">The current round number.</param>
        void Reset(int currentRound);

        /// <summary>
        /// Gets the highest score achieved.
        /// </summary>
        /// <returns>The highest score.</returns>
        int GetHighestScore();
    }
}