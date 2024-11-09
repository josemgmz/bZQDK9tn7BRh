using System.Threading.Tasks;
using VContainer;

namespace Game.Services.Impl
{
    public class GameServiceImpl : IGameService
    {
        #region Variables
        
        [Inject] private ILogService _logService;
        [Inject] private IAudioService _audioService;
        [Inject] private IRoundService _roundService;
        
        private const string BACKGROUND_MUSIC = "backgroundMusic.mp3";

        #endregion

        #region Lifecycle
        
        public async void Start()
        {
            SetupMusic();
            _logService.Log("GameService initialized");
            await Task.Delay(250);
            _roundService.StartRound();
        }

        public void Tick()
        {
            
        }

        #endregion

        #region Private Methods

        private void SetupMusic()
        { 
            _audioService.SetMusicVolume(-35.0f);
            _audioService.SetSfxVolume(-10.0f);
            _audioService.PlayMusic(BACKGROUND_MUSIC, true);
        }

        #endregion
    }
}