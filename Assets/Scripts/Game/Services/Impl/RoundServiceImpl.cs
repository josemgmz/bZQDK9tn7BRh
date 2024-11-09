
using Framework;
using VContainer;

namespace Game.Services.Impl
{
    public class RoundServiceImpl : IRoundService
    {
        #region Variables

        private int _roundNumber = 0;
        
        [Inject] private ILevelService _levelService;
        [Inject] private IGameEventBus _gameEventBus;

        #endregion
        
        #region Public Methods

        public void StartRound()
        {
            var level = _levelService.GetLevel(_roundNumber);
            _gameEventBus.RaiseEvent(level);
        }

        public void EndRound(bool victory)
        {
            if(victory) _roundNumber++;
            StartRound();
        }


        #endregion

       
    }
}