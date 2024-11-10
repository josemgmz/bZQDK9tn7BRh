using System;
using Framework;
using TMPro;
using UnityEngine;

namespace Game.Entities.UITextElement
{
    [Serializable]
    public class UITextElementModel : GameModel
    {
        #region Variables

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _value;

        #endregion

        #region Properties

        public TextMeshProUGUI Title
        {
            get => _title;
            set => _title = value;
        }

        public TextMeshProUGUI Value
        {
            get => _value;
            set => _value = value;
        }

        #endregion
    }
}