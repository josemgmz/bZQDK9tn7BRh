using Framework;
using UnityEngine;

namespace Game.Entities.CardGrid
{
    public class CardGridView : GameViewUI
    {
        #region Variables

        [GameFieldAttributes.ModelField, SerializeField] private CardGridModel _model;
        [GameFieldAttributes.ControllerField] private CardGridController _controller;

        #endregion
    }
}