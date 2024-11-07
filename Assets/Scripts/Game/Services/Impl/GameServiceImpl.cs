using VContainer;

namespace Game.Services.Impl
{
    public class GameServiceImpl : IGameService
    {
        #region Variables
        
        [Inject] private ILogService _logService;

        #endregion

        #region Lifecycle
        
        public void Start()
        {
            _logService.Log("GameService initialized");
        }

        #endregion

        public void Tick()
        {
            
        }
    }
}