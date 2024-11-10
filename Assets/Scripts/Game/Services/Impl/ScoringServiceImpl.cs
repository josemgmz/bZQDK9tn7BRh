using System.Collections.Generic;
using Framework;
using Game.Entities.UITextElement.Data;
using VContainer;

namespace Game.Services.Impl
{
    public class ScoringServiceImpl : IScoringService
    {
        #region Variables

        private const int MATCHING_SCORE = 250;
        private const int MISS_SCORE = -50;
        private List<int> _scores = new();
        private int _combo = 0;
        private int _currentScore = 0;
        private int _turns = 0;
        private int _miss = 0;
        
        [Inject] private IGameEventBus _gameEventBus;

        #endregion

        #region Public Methods

        public void Match()
        {
            _combo++;
            _turns++;
            _currentScore += MATCHING_SCORE * _combo;
            UpdateUI();
        }
        
        public void Miss()
        {
            _combo = 0;
            _miss++;
            _turns++;
            UpdateUI();
        }
        
        public void Reset()
        {
            _currentScore -= _miss * MISS_SCORE;
            _currentScore = _currentScore < 0 ? 0 : _currentScore;
            _scores.Add(_currentScore);
            _combo = 0;
            _currentScore = 0;
            _turns = 0;
            _miss = 0;
            UpdateUI(true);
        }

        #endregion

        #region Private Methods

        private void UpdateUI(bool resetTime = false)
        {
            var eventData = new OnUIManagerUpdateEvent
            {
                ResetTime = resetTime,
                Combo = _combo,
                Score = _currentScore,
                Turns = _turns
            };
            _gameEventBus.RaiseEvent(eventData);
        }

        #endregion
    }
}