using Game.Entities.CardGrid;
using Game.Entities.CardGrid.Data;
using Game.Services.Lifetime;

namespace Game.Services
{
    /// <summary>
    /// Provides game-related functionalities and lifecycle management.
    /// </summary>
    public interface ILevelService: IStartableService, IStoppableService
    {
        /// <summary>
        /// Gets the level data for the specified level.
        /// </summary>
        /// <param name="level">The level number.</param>
        /// <returns>The level data.</returns>
        public OnCardGridSetupEvent GetLevel(int level);
    }
}