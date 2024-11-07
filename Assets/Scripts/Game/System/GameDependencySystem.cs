using Framework;
using Game.Services;
using Game.Services.Impl;
using Game.Services.Lifetime;
using VContainer;
using VContainer.Unity;

namespace Game.System
{
    public class GameDependencySystem : GameDependency
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //Register Services
            builder.Register<IGameEventBus, GameEventBusImpl>(Lifetime.Singleton);
            builder.Register<ILogService, LogServiceImpl>(Lifetime.Singleton);
            builder.Register<IStartableService, IGameService, GameServiceImpl>(Lifetime.Singleton);
            
            //Register EntryPoint
            builder.RegisterEntryPoint<EntryPointSystem>();
        }
    }
}