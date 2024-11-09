using Framework;
using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.Entities.Card
{
    public class CardImageController : GameController<CardView>
    {
        #region Services

        [Inject] private IGameAddressablesService _gameAddressablesService;

        #endregion
        
        #region Lifecycle
        
        private void Start()
        {
            //Get model
            var model = GetModel<CardModel>();
            model.FrontSprite = GetFrontSprite(model.CardShape, model.CardType);
            model.BackSprite = GetBackSprite();

        }

        #endregion

        #region Private Methods

        private Sprite GetFrontSprite(CardShape cardShape, CardType cardType)
        {
            return _gameAddressablesService.GetCardSprite(cardShape, cardType);
        }
        
        private Sprite GetBackSprite()
        {
            return _gameAddressablesService.GetCardSprite(CardShape.Back);
        }

        #endregion
    }
}