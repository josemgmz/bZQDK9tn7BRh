using Framework;
using UnityEngine;

namespace Game.Entities.UITextElement
{
    public class UITextElementView : GameViewUI
    {
        #region Variables

        [GameFieldAttributes.ModelField, SerializeField] private UITextElementModel _model;
        [GameFieldAttributes.ControllerField] private UITextElementController _controller;

        #endregion
    }
}