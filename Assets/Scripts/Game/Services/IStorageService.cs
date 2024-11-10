using Game.Services.Lifetime;

namespace Game.Services
{
    public interface IStorageService : IStartableService
    {
        void AddData(int score, int turns, int combo, int round);
        void ClearData();
        int GetLastRound();
    }
}