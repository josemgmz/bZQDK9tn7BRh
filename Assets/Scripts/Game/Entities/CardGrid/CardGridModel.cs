using System;
using Framework;
using Game.Helper;
using UnityEngine;

namespace Game.Entities.CardGrid
{
    [Serializable]
    public class CardGridModel : GameModel
    {
        #region Variables

        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private ResponsiveGridLayout responsiveGridLayout;
        [SerializeField] private Vector2 spacing = new (25, 25);

        #endregion

        #region Properties

        public GameObject CardPrefab => cardPrefab;
        public ResponsiveGridLayout ResponsiveGridLayout => responsiveGridLayout;
        public Vector2 Spacing => spacing;

        #endregion
    }
}