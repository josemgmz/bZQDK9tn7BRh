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
            builder.Register<IStartableService, IStorageService, StorageServiceImpl>(Lifetime.Singleton);
            builder.Register<ILogService, LogServiceImpl>(Lifetime.Singleton);
            builder.Register<IStartableService, IStoppableService, IAudioService, AudioServiceImpl>(Lifetime.Singleton);
            builder.Register<IStoppableService, IGameAddressablesService, GameAddressablesServiceImpl>(Lifetime.Singleton);
            builder.Register<IStartableService, IStoppableService, ILevelService, LevelServiceImpl>(Lifetime.Singleton);
            builder.Register<IStartableService, IRoundService, RoundServiceImpl>(Lifetime.Singleton);
            builder.Register<IScoringService, ScoringServiceImpl>(Lifetime.Singleton);
            
            //Register Game Service
            builder.Register<IStartableService, IStoppableService, IGameService, GameServiceImpl>(Lifetime.Singleton);
            
            //Register EntryPoint
            builder.RegisterEntryPoint<EntryPointSystem>();
        }
    }
}