﻿
using System.Threading.Tasks;
using Framework;
using Game.Services.Data;
using VContainer;

namespace Game.Services.Impl
{
    public class RoundServiceImpl : IRoundService
    {
        #region Variables

        private int _roundNumber = 0;
        private int _pairsMatched = 0;
        private bool _roundInProgress = false;
        
        [Inject] private ILevelService _levelService;
        [Inject] private IGameEventBus _gameEventBus;
        [Inject] private IScoringService _scoringService;
        [Inject] private ILogService _logService;
        [Inject] private IStorageService _storageService;

        #endregion

        #region Lifecycle
        
        public void Start()
        {
            _roundNumber = _storageService.GetLastRound();
        }

        #endregion
        
        #region Public Methods

        public async void StartRound()
        {
            _roundInProgress = true;
            var level = _levelService.GetLevel(_roundNumber);
            _pairsMatched = 0;
            _gameEventBus.RaiseEvent(level);
            _logService.Log($"Round {_roundNumber} started");
            await Task.Delay(2500);
            _gameEventBus.RaiseEvent(new OnRoundStartEvent());
        }

        public async void EndRound(bool victory)
        {
            await Task.Delay(500);
            _logService.Log($"Round {_roundNumber} ended");
            _scoringService.Reset(_roundNumber);
            var eventData = new OnRoundEndEvent
            {
                lastRound = _roundNumber
            };
            if(victory) _roundNumber++;
            eventData.currentRound = _roundNumber;
            _gameEventBus.RaiseEvent(eventData);
            await Task.Delay(1000);
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
            _scoringService.Match();
        }
        
        public void Miss()
        {
            _scoringService.Miss();
        }
        
        public bool RoundInProgress()
        {
            return _roundInProgress;
        }

        #endregion
    }
}