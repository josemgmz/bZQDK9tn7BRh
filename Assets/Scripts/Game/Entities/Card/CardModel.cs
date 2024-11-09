using System;
using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities.Card
{
    [Serializable]
    public class CardModel : GameModel
    {
        #region Constants
        
        public const string CARD_FLIP_SFX = "cardFlip.mp3";

        #endregion
        
        #region Variables

        [Header("Components")]
        [SerializeField] private Image image;
        
        [Header("Reference")]
        [SerializeField] private Sprite frontSprite;
        [SerializeField] private Sprite backSprite;
        
        [Header("Configurations")]
        [Range(0f, 1f)] [SerializeField] private float rotationTime = 0.25f;
        [SerializeField] private CardType cardType;
        [SerializeField] private CardShape cardShape;
        [SerializeField] private CardColor cardColor;
        [SerializeField] private bool isFlipped;
        
        private Coroutine _flipCardCoroutine;

        #endregion

        #region Properties

        public Image Image => image;
        public float RotationTime => rotationTime;
        public Sprite FrontSprite => frontSprite;
        public Sprite BackSprite => backSprite;
        public Coroutine FlipCardCoroutine
        {
            get => _flipCardCoroutine;
            set => _flipCardCoroutine = value;
        }
        public bool IsFlipped
        {
            get => isFlipped;
            set => isFlipped = value;
        }

        #endregion

        #region Methods

        public void Initialize<T>(params object[] value)
        {
            cardShape = (CardShape) value[0];
            cardColor = (CardColor) value[1];
            cardType = (CardType) value[2];
            isFlipped = (bool) value[3];
        }

        #endregion
    }
}