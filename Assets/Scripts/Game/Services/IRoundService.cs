using Game.Services.Lifetime;

namespace Game.Services
{
    public interface IRoundService: IStartableService, IStoppableService
    {
        void StartRound();
        void EndRound(bool victory);
        void SetRound(int round);
        int PairsToMatch();
        int PairsMatched();
        bool RoundInProgress();
        int GetLastRound();
    }
}