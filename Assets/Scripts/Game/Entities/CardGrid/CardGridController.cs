using System.Collections.Generic;
using Framework;
using Game.Entities.Card;
using Game.Helper;
using UnityEngine;
using VContainer;

namespace Game.Entities.CardGrid
{
    public class CardGridController : GameController<GameViewUI>
    {
        #region Lifecycle

        [Inject] private IGameEventBus _eventBus;

        #endregion
        
        #region Lifecycle Methods

        private void Awake()
        {
            _eventBus.AddListener<CardGridSetupRequest>(CreateGrid);
        }
        
        private void OnDestroy()
        {
            _eventBus.RemoveListener<CardGridSetupRequest>(CreateGrid);
        }

        #endregion

        #region Methods

        private void CreateGrid(CardGridSetupRequest request)
        {
            //Get model
            var model = GetModel<CardGridModel>();
            model.ResponsiveGridLayout.SetColumns(request.Columns);
            model.ResponsiveGridLayout.SetSpacing(model.Spacing);
            
            //Create the grid.
            request.Cards.ForEach(it =>
            {
                CreateCard(model.CardPrefab, it);
                CreateCard(model.CardPrefab, it);
            });
            
            //Shuffle the cards
            if (!request.Shuffle) return;
            var children = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++)
                children.Add(transform.GetChild(i));
            children.Shuffle();
            children.ForEach(it => it.SetSiblingIndex(0));
        }

        private void CreateCard(GameObject cardPrefab, CardSetupRequest it)
        {
            //Instantiate the card
            var card = Object.Instantiate(cardPrefab, transform);
            card.name = $"Card {it.CardShape.GetString()} {it.CardType.GetString()}";
                
            //Get the view and initialize it
            var cardView = card.GetComponent<CardView>();
            cardView.Initialize<CardModel>(it.CardShape, it.CardType, false);
        }

        #endregion
    }
}