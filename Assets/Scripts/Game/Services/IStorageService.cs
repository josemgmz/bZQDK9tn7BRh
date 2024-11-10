using System.Collections.Generic;
using Game.Services.Data;
using Game.Services.Lifetime;

namespace Game.Services
{
    /// <summary>
    /// Provides functionalities to store and retrieve game data from the user local device.
    /// </summary>
    public interface IStorageService : IStartableService
    {
        /// <summary>
        /// Adds the specified data to the storage.
        /// </summary>
        /// <param name="score">The score to add.</param>
        /// <param name="turns">The number of turns to add.</param>
        /// <param name="combo">The combo count to add.</param>
        /// <param name="round">The round number to add.</param>
        void AddData(int score, int turns, int combo, int round);

        /// <summary>
        /// Clears all stored data.
        /// </summary>
        void ClearData();

        /// <summary>
        /// Gets the stored data.
        /// </summary>
        /// <returns>A list of stored data.</returns>
        List<ScoringData> GetData();
    }
}