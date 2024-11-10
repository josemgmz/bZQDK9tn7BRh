using Framework;
using Game.Entities.Card.Data;
using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.Entities.Card
{
    /// <summary>
    /// Controls the card image, handling the front and back sprites and flipping the card.
    /// </summary>
    public class CardImageController : GameController<CardView>
    {
        #region Services

        /// <summary>
        /// Service for managing addressable assets.
        /// </summary>
        [Inject] private IGameAddressablesService _gameAddressablesService;

        #endregion

        #region Lifecycle

        /// <summary>
        /// Initializes the card image controller.
        /// </summary>
        private void Start()
        {
            // Get model
            var model = GetModel<CardModel>();
            model.FrontSprite = GetFrontSprite(model.CardShape, model.CardType);
            model.BackSprite = GetBackSprite();

            if (model.IsFlipped)
            {
                model.Image.sprite = model.FrontSprite;
                return;
            }
            model.Image.sprite = model.BackSprite;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Flips the card image between the front and back sprites.
        /// </summary>
        public void FlipCard()
        {
            var model = GetModel<CardModel>();
            model.Image.sprite = model.Image.sprite == model.BackSprite ? model.FrontSprite : model.BackSprite;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the front sprite for the card based on its shape and type.
        /// </summary>
        /// <param name="cardShape">The shape of the card.</param>
        /// <param name="cardType">The type of the card.</param>
        /// <returns>The front sprite of the card.</returns>
        private Sprite GetFrontSprite(CardShape cardShape, CardType cardType)
        {
            return _gameAddressablesService.GetCardSprite(cardShape, cardType);
        }

        /// <summary>
        /// Gets the back sprite for the card.
        /// </summary>
        /// <returns>The back sprite of the card.</returns>
        private Sprite GetBackSprite()
        {
            return _gameAddressablesService.GetCardSprite(CardShape.Back);
        }

        #endregion
    }
}