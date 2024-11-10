using Game.Services.Lifetime;

namespace Game.Services
{
    /// <summary>
    /// Provides functionalities to manage game rounds.
    /// </summary>
    public interface IRoundService: IStartableService, IStoppableService
    {
        /// <summary>
        /// Starts a new round.
        /// </summary>
        void StartRound();

        /// <summary>
        /// Ends the current round.
        /// </summary>
        /// <param name="victory">Whether the round ended in victory.</param>
        void EndRound(bool victory);

        /// <summary>
        /// Sets the current round number.
        /// </summary>
        /// <param name="round">The round number to set.</param>
        void SetRound(int round);

        /// <summary>
        /// Gets the number of pairs to match in the current round.
        /// </summary>
        /// <returns>The number of pairs to match.</returns>
        int PairsToMatch();

        /// <summary>
        /// Gets the number of pairs matched in the current round.
        /// </summary>
        /// <returns>The number of pairs matched.</returns>
        int PairsMatched();

        /// <summary>
        /// Checks if a round is currently in progress.
        /// </summary>
        /// <returns>True if a round is in progress, otherwise false.</returns>
        bool RoundInProgress();

        /// <summary>
        /// Gets the last round number.
        /// </summary>
        /// <returns>The last round number.</returns>
        int GetLastRound();

        /// <summary>
        /// Gets the current round number.
        /// </summary>
        /// <returns>The current round number.</returns>
        int GetRound();
    }
}