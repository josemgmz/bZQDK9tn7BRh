
using Game.Services.Lifetime;

namespace Game.Services
{
    /// <summary>
    /// Provides game-related functionalities and lifecycle management.
    /// </summary>
    public interface IGameService : IStartableService, IStoppableService
    {
    }
}