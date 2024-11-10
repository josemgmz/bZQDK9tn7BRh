using Framework;
using UnityEngine;

namespace Game.Entities.UIManager
{
    public class UIManagerView : GameViewUI
    {
        #region Variables

        [GameFieldAttributes.ModelField, SerializeField] private UIManagerModel _model;
        [GameFieldAttributes.ControllerField] private UIManagerController _controller;

        #endregion
    }
}