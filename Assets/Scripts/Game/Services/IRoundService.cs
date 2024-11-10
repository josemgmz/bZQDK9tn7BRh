using Game.Services.Lifetime;

namespace Game.Services
{
    public interface IRoundService
    {
        void StartRound();
        void EndRound(bool victory);
        void SetRound(int round);
        int PairsToMatch();
        int PairsMatched();
        void Match();
        void Miss();
    }
}