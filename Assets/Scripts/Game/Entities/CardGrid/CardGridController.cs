using System.Collections.Generic;
using Framework;
using Game.Entities.Card;
using Game.Entities.Card.Data;
using Game.Entities.CardGrid.Data;
using Game.Helper;
using Game.Services.Data;
using UnityEngine;
using VContainer;

namespace Game.Entities.CardGrid
{
    /// <summary>
    /// Controls the card grid, handling the creation and cleanup of the grid.
    /// </summary>
    public class CardGridController : GameController<GameViewUI>
    {
        #region Lifecycle

        /// <summary>
        /// Event bus for handling game events.
        /// </summary>
        [Inject] private IGameEventBus _eventBus;

        #endregion

        #region Lifecycle Methods

        /// <summary>
        /// Registers event listeners for setting up and cleaning the grid.
        /// </summary>
        private void Awake()
        {
            _eventBus.AddListener<OnCardGridSetupEvent>(CreateGrid);
            _eventBus.AddListener<OnRoundEndEvent>(CleanGrid);
        }

        /// <summary>
        /// Unregisters event listeners when the controller is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            _eventBus.RemoveListener<OnCardGridSetupEvent>(CreateGrid);
            _eventBus.RemoveListener<OnRoundEndEvent>(CleanGrid);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the card grid based on the provided event data.
        /// </summary>
        /// <param name="eventData">The event data containing grid setup information.</param>
        private void CreateGrid(OnCardGridSetupEvent eventData)
        {
            // Get model
            var model = GetModel<CardGridModel>();
            model.ResponsiveGridLayout.SetColumns(eventData.Columns);
            model.ResponsiveGridLayout.SetSpacing(model.Spacing);

            // Create the grid
            eventData.Cards.ForEach(it =>
            {
                CreateCard(model.CardPrefab, it);
                CreateCard(model.CardPrefab, it);
            });

            // Shuffle the cards
            if (!eventData.Shuffle) return;
            var children = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++)
                children.Add(transform.GetChild(i));
            children.Shuffle();
            children.ForEach(it => it.SetSiblingIndex(0));
        }

        /// <summary>
        /// Creates a card in the grid.
        /// </summary>
        /// <param name="cardPrefab">The card prefab to instantiate.</param>
        /// <param name="it">The event data for setting up the card.</param>
        private void CreateCard(GameObject cardPrefab, OnCardSetupEvent it)
        {
            // Instantiate the card
            var card = Object.Instantiate(cardPrefab, transform);
            card.name = $"Card {it.CardShape.GetString()} {it.CardType.GetString()}";

            // Get the view and initialize it
            var cardView = card.GetComponent<CardView>();
            cardView.Initialize<CardModel>(it.CardShape, it.CardType, true);
        }

        /// <summary>
        /// Cleans up the card grid by destroying all child objects.
        /// </summary>
        /// <param name="eventData">The event data for ending the round.</param>
        private void CleanGrid(OnRoundEndEvent eventData)
        {
            // Destroy
            for (var i = 0; i < transform.childCount; i++)
                Object.Destroy(transform.GetChild(i).gameObject);
        }

        #endregion
    }
}