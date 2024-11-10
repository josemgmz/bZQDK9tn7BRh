using Game.Entities.CardGrid;
using Game.Services.Lifetime;

namespace Game.Services
{
    public interface ILevelService: IStartableService, IStoppableService
    {
        public OnCardGridSetupEvent GetLevel(int level);
    }
}