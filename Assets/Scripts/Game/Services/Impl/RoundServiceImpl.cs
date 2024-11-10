
using System.Threading.Tasks;
using Framework;
using Game.Entities.Card;
using Game.Entities.CardGrid;
using VContainer;

namespace Game.Services.Impl
{
    public class RoundServiceImpl : IRoundService
    {
        #region Variables

        private int _roundNumber = 0;
        private int _pairsMatched = 0;
        
        [Inject] private ILevelService _levelService;
        [Inject] private IGameEventBus _gameEventBus;
        [Inject] private ILogService _logService;

        #endregion
        
        #region Public Methods

        public async void StartRound()
        {
            var level = _levelService.GetLevel(_roundNumber);
            _pairsMatched = 0;
            _gameEventBus.RaiseEvent(level);
            _logService.Log($"Round {_roundNumber} started");
            await Task.Delay(2500);
            _gameEventBus.RaiseEvent(new OnCardFlipEvent());
        }

        public void EndRound(bool victory)
        {
            _logService.Log($"Round {_roundNumber} ended");
            if(victory) _roundNumber++;
            _gameEventBus.RaiseEvent(new OnCardCleanEvent{});
            StartRound();
        }

        public void SetRound(int round)
        {
            _pairsMatched = 0;
            _roundNumber = round;
            _logService.Log($"Round set to {_roundNumber}");
        }
        
        public int PairsToMatch()
        {
            return _levelService.GetLevel(_roundNumber).Cards.Count;
        }
        
        public int PairsMatched()
        {
            return _pairsMatched;
        }
        
        public void Match()
        {
            _pairsMatched++;
        }

        #endregion

       
    }
}