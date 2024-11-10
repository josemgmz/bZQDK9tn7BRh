
using Game.Services.Lifetime;

namespace Game.Services
{
    public interface IScoringService : IStartableService, IStoppableService
    {
        void Reset(int currentRound);
        int GetHighestScore();
    }
}