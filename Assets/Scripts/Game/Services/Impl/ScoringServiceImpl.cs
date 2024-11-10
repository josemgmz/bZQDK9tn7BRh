using System.Linq;
using Framework;
using Game.Entities.UITextElement.Data;
using Game.Services.Data;
using VContainer;

namespace Game.Services.Impl
{
    public class ScoringServiceImpl : IScoringService
    {
        #region Variables

        private const int MATCHING_SCORE = 250;
        private const int MISS_SCORE = -50;
        private int _currentScore = 0;
        private int _combo = 0;
        private int _turns = 0;
        private int _miss = 0;
        
        [Inject] private IGameEventBus _gameEventBus;
        [Inject] private IStorageService _storageService;

        #endregion

        #region Lifecycle

        public void Start()
        {
            _gameEventBus.AddListener<OnRoundCardMatchEvent>(Match);
            _gameEventBus.AddListener<OnRoundCardMissMatchEvent>(Miss);
        }

        public void Stop()
        {
            _gameEventBus.RemoveListener<OnRoundCardMatchEvent>(Match);
            _gameEventBus.RemoveListener<OnRoundCardMissMatchEvent>(Miss);
        }

        #endregion

        #region Public Methods
        
        public void Reset(int currentRound)
        {
            _currentScore -= _miss * MISS_SCORE;
            _currentScore = _currentScore < 0 ? 0 : _currentScore;;
            _storageService.AddData(_currentScore, _turns, _combo, currentRound);
            _combo = 0;
            _currentScore = 0;
            _turns = 0;
            _miss = 0;
            UpdateUI(true);
        }
        
        public int GetHighestScore()
        {
            var scoringData = _storageService.GetData();
            return scoringData.Count > 0 ? scoringData.Max(it => it.Score) : 0;
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

        #region Private Methods
        
        private void Match()
        {
            _combo++;
            _turns++;
            _currentScore += MATCHING_SCORE * _combo;
            UpdateUI();
        }
        
        private void Miss()
        {
            _combo = 0;
            _miss++;
            _turns++;
            UpdateUI();
        }

        #endregion
    }
}