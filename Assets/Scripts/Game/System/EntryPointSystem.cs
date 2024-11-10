using System;
using System.Collections.Generic;
using Game.Services.Lifetime;
using VContainer;
using VContainer.Unity;
namespace Game.System
{
    public class EntryPointSystem : IInitializable, ITickable, IDisposable
    {
        #region Variables

        [Inject] private IEnumerable<IStartableService> _startableServices;
        [Inject] private IEnumerable<ITickableService> _tickableServices;
        [Inject] private IEnumerable<IStoppableService> _stoppableServices;

        #endregion
        
        #region Lifecycle

        public void Initialize()
        {
            foreach (var startableService in _startableServices)
            {
                startableService.Start();
            }
        }
        
        public void Tick()
        {
            foreach (var tickableService in _tickableServices)
            {
                tickableService.Tick();
            }
        }

        public void Dispose()
        {
            foreach (var stoppableService in _stoppableServices)
            {
                stoppableService.Stop();
            }
        }

        #endregion
    }
}