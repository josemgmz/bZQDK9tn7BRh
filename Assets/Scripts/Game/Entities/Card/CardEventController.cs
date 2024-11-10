using System.Threading.Tasks;
using Framework;
using VContainer;

namespace Game.Entities.Card
{
    public class CardEventController : GameController<CardView>
    {
        #region Services
        
        [Inject] private IGameEventBus _gameEventBus;

        #endregion
        
        #region Public Methods

        public void SendOnCardFlippedEvent()
        {
            var model = GetModel<CardModel>();
            var eventData = new OnCardFlippedEvent
            {
                CardShape = model.CardShape,
                CardType = model.CardType,
                OnFail = OnFail,
                OnSuccess = OnSuccess
            };
            _gameEventBus.RaiseEvent(eventData);
        }

        #endregion

        #region Private Methods
        
        private async void OnFail()
        {
            await Task.Delay(250);
            GetController<CardAnimationController>().FlipCard();
        }
        
        private void OnSuccess()
        {
            
        }

        #endregion
    }
}