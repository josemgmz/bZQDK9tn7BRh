using System;
using Framework;
using Game.Entities.UITextElement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities.UIManager
{
    [Serializable]
    public class UIManagerModel : GameModel
    {
        #region Variables

        [SerializeField] private UITextElementView _time;
        [SerializeField] private UITextElementView _turns;
        [SerializeField] private UITextElementView _combo;
        [SerializeField] private UITextElementView _score;
        [SerializeField] private Button _settingsButton;
        
        private UITextElementController _timeController;
        private UITextElementController _turnsController;
        private UITextElementController _comboController;
        private UITextElementController _scoreController;
        private float _startTime;

        #endregion
        
        #region Properties
        
        public Button SettingsButton => _settingsButton;
        
        public float StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }
        public UITextElementView Time => _time;
        public UITextElementView Turns => _turns;
        public UITextElementView Combo => _combo;
        public UITextElementView Score => _score;
        
        public UITextElementController TimeController
        {
            get => _timeController;
            set => _timeController = value;
        }

        public UITextElementController TurnsController
        {
            get => _turnsController;
            set => _turnsController = value;
        }

        public UITextElementController ComboController
        {
            get => _comboController;
            set => _comboController = value;
        }

        public UITextElementController ScoreController
        {
            get => _scoreController;
            set => _scoreController = value;
        }

        #endregion
    }
}