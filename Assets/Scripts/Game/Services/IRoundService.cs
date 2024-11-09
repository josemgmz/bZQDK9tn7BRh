using Game.Services.Lifetime;

namespace Game.Services
{
    public interface IRoundService
    {
        void StartRound();
        void EndRound(bool victory);
    }
}