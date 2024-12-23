﻿using Framework;
using UnityEngine;

namespace Game.Entities.Card
{
    public class CardView : GameViewUI
    {
        #region Variables

        [GameFieldAttributes.ModelField, SerializeField] private CardModel _model;
        [GameFieldAttributes.ControllerField] private CardImageController _imageController;
        [GameFieldAttributes.ControllerField] private CardAnimationController _animationController;
        [GameFieldAttributes.ControllerField] private CardEventController _eventController;

        #endregion
    }
}