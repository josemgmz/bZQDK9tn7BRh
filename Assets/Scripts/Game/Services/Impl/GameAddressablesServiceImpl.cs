using System;
using System.Collections.Generic;
using Framework;
using Game.Entities.Card;
using Game.Entities.Card.Data;
using UnityEngine;

namespace Game.Services.Impl
{
    public class GameAddressablesServiceImpl : IGameAddressablesService
    {
        #region Variables
        
        private const string CARD_ASSET_PATH = "Assets/Content/Textures/Cards/";

        private readonly GameAddressableContext _gameAddressableContext = new();
        private Dictionary<Tuple<CardType, CardShape>, Sprite> _cardSprites = new();

        #endregion

        #region Public Methods

        public Sprite GetCardSprite(CardShape cardShape, CardType cardType)
        {
            var key = new Tuple<CardType, CardShape>(cardType, cardShape);
            if (_cardSprites.TryGetValue(key, out var cardSprite))
            {
                return cardSprite;
            }

            var cardPath = $"{CARD_ASSET_PATH}{cardShape.GetString().ToLower()}{cardType.GetString().ToLower()}.png";
            var sprite = _gameAddressableContext.LoadAsset<Sprite>(cardPath);
            _cardSprites.Add(key, sprite);
            return sprite;
        }

        #endregion

        public void Stop()
        {
            _cardSprites.Clear();
            _gameAddressableContext.Release();
        }
    }
}