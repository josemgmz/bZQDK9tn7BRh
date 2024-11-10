using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using Game.Entities.Card;
using Game.Services.Data;
using VContainer;

namespace Game.Services.Impl
{
    public class GameServiceImpl : IGameService
    {
        #region Variables
        
        [Inject] private ILogService _logService;
        [Inject] private IAudioService _audioService;
        [Inject] private IRoundService _roundService;
        [Inject] private IGameEventBus _gameEventBus;
        
        private List<OnCardFlippedEvent> _cardFlippedEvents = new List<OnCardFlippedEvent>();

        #endregion

        #region Lifecycle
        
        public async void Start()
        {
            SetupMusic();
            _gameEventBus.AddListener<OnCardFlippedEvent>(OnCardFlippedEvent);
            
            await Task.Delay(250);
            _roundService.StartRound();
            _logService.Log("GameService initialized");
        }

        public void Tick()
        {
            
        }

        public void Stop()
        {
            _gameEventBus.RemoveListener<OnCardFlippedEvent>(OnCardFlippedEvent);
            _logService.Log("GameService stopped");
        }

        #endregion

        #region Private Methods

        private void SetupMusic()
        { 
            _audioService.SetMusicVolume(-35.0f);
            _audioService.SetSfxVolume(-10.0f);
            _audioService.PlayMusic(AudioData.Music.Background, true);
        }
        
        private void OnCardFlippedEvent(OnCardFlippedEvent eventData)
        {
            _logService.Log("Card flipped event received");
            _cardFlippedEvents.Add(eventData);
            if (_cardFlippedEvents.Count <= 1) return;
            var firstEvent = _cardFlippedEvents[0];
            var secondEvent = _cardFlippedEvents[1];
            
            _cardFlippedEvents.Remove(firstEvent);
            _cardFlippedEvents.Remove(secondEvent);
            
            if (firstEvent.CardShape == secondEvent.CardShape && firstEvent.CardType == secondEvent.CardType)
            {
                firstEvent.OnSuccess();
                secondEvent.OnSuccess();
                _audioService.PlaySfx(AudioData.Sfx.SuccessMatching);
            }
            else
            {
                firstEvent.OnFail();
                secondEvent.OnFail();
                _audioService.PlaySfx(AudioData.Sfx.ErrorMatching);
            }
        }

        #endregion
    }
}