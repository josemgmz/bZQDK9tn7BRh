
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
        [Inject] private ILogService _logService;

        #endregion
        
        #region Public Methods

        public void StartRound()
        {
            var level = _levelService.GetLevel(_roundNumber);
            _gameEventBus.RaiseEvent(level);
            _logService.Log($"Round {_roundNumber} started");
        }

        public void EndRound(bool victory)
        {
            _logService.Log($"Round {_roundNumber} ended");
            if(victory) _roundNumber++;
            StartRound();
        }

        public void SetRound(int round)
        {
            _roundNumber = round;
            _logService.Log($"Round set to {_roundNumber}");
        }

        #endregion

       
    }
}