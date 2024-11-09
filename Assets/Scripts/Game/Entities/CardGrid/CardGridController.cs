using System.Collections.Generic;
using Framework;
using Game.Entities.Card;
using UnityEngine;

namespace Game.Entities.CardGrid
{
    public class CardGridController : GameController<GameViewUI>
    {
        #region Lifecycle Methods

        private void Awake()
        {
            var exampleList = new List<CardSetupRequest>
            {
                new CardSetupRequest { CardShape = CardShape.Club, CardType = CardType.Eight },
                new CardSetupRequest{ CardShape = CardShape.Diamond, CardType = CardType.Five },
            };
            CreateGrid(2, exampleList);
        }

        #endregion

        #region Methods

        private void CreateGrid(int columns, List<CardSetupRequest> cards)
        {
            //Get model
            var model = GetModel<CardGridModel>();
            model.ResponsiveGridLayout.SetColumns(columns);
            model.ResponsiveGridLayout.SetSpacing(model.Spacing);
            
            //Create the grid.
            cards.ForEach(it =>
            {
                CreateCard(model.CardPrefab, it);
                CreateCard(model.CardPrefab, it);
            });
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