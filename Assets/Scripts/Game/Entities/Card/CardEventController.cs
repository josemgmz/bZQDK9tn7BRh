using System.Threading.Tasks;
using Framework;
using Game.Entities.Card.Data;
using Game.Services.Data;
using VContainer;

namespace Game.Entities.Card
{
    /// <summary>
    /// Controls the card events, handling card flip and match events.
    /// </summary>
    public class CardEventController : GameController<CardView>
    {
        #region Services

        /// <summary>
        /// Event bus for handling game events.
        /// </summary>
        [Inject] private IGameEventBus _gameEventBus;

        #endregion

        #region Lifecycle

        /// <summary>
        /// Registers event listeners when the controller is awakened.
        /// </summary>
        private void Awake()
        {
            _gameEventBus.AddListener<OnRoundStartEvent>(OnCardFlipEvent);
        }

        /// <summary>
        /// Unregisters event listeners when the controller is destroyed.
        /// </summary>
        public void OnDestroy()
        {
            _gameEventBus.RemoveListener<OnRoundStartEvent>(OnCardFlipEvent);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends the card flipped event.
        /// </summary>
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

        /// <summary>
        /// Handles the failure of a card flip event.
        /// </summary>
        private async void OnFail()
        {
            await Task.Delay(250);
            GetController<CardAnimationController>().FlipCard();
        }

        /// <summary>
        /// Handles the success of a card flip event.
        /// </summary>
        private async void OnSuccess()
        {
            await Task.Delay(250);
            GetController<CardAnimationController>().MatchCard();
        }

        /// <summary>
        /// Handles the card flip event.
        /// </summary>
        private void OnCardFlipEvent()
        {
            GetController<CardAnimationController>().FlipCard(false);
        }

        #endregion
    }
}