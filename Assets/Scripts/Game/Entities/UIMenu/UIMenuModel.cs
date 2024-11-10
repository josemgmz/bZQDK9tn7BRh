using System;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities.UIMenu
{
    [Serializable]
    public class UIMenuModel : GameModel
    {
        #region Variables

        [Header("UI Elements")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _clearProgressButton;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _lastRound;
        [SerializeField] private RectTransform _content;
        [SerializeField] private RectTransform _leftPanel;
        [SerializeField] private RectTransform _rightPanel;

        #endregion

        #region Properties
        
        public Button PlayButton => _playButton;
        public Button ClearProgressButton => _clearProgressButton;
        public TextMeshProUGUI Score => _score;
        public TextMeshProUGUI LastRound => _lastRound;
        public RectTransform Content => _content;
        public RectTransform LeftPanel => _leftPanel;
        public RectTransform RightPanel => _rightPanel;

        #endregion
    }
}