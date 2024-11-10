using Framework;
using UnityEngine;

namespace Game.Entities.UIMenu
{
    public class UIMenuView : GameViewUI
    {
        #region Variables

        [GameFieldAttributes.ModelField, SerializeField] private UIMenuModel _model;
        [GameFieldAttributes.ControllerField] private UIMenuController _controller;

        #endregion
    }
}