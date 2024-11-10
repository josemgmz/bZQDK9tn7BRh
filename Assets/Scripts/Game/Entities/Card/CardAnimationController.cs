﻿using System.Collections;
using Framework;
using Game.Helper;
using Game.Services;
using Game.Services.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Game.Entities.Card
{
    public class CardAnimationController : GameController<CardView>
    {
        #region Services

        [Inject] private IAudioService _audioService;

        #endregion
        
        #region Lifecycle

        private void Start()
        {
            //Setup the face of the card
            var model = GetModel<CardModel>();
            if (model.IsFlipped) return;
            transform.rotation *= Quaternion.Euler(0, -180, 0);
        }

        #endregion
        
        #region Events

        private void OnPointerUp(PointerEventData other)
        {
            var model = GetModel<CardModel>();
            if (model.IsFlipped) return;
            FlipCard();
        }

        #endregion

        #region Public Methods

        public void FlipCard()
        {
            var model = GetModel<CardModel>();
            if(model.FlipCardCoroutine != null) return;
            model.FlipCardCoroutine = StartCoroutine(_FlipCard());
        }

        #endregion

        #region Private Methods

        private IEnumerator _FlipCard()
        {
            _audioService.PlaySfx(AudioData.Sfx.CardFlip);
            var model = GetModel<CardModel>();
            var duration = model.RotationTime;
            var startRotation = transform.rotation;
            var midRotation = startRotation * Quaternion.Euler(0, -90, 0);
            var endRotation = startRotation * Quaternion.Euler(0, -180, 0);

            yield return transform.RotateTo(startRotation, midRotation, duration / 2);

            // Set rotation to exactly 90 degrees and change sprite
            transform.rotation = midRotation;
            GetController<CardImageController>().FlipCard();

            yield return transform.RotateTo(midRotation, endRotation, duration / 2);

            // Set rotation to exactly 180 degrees
            transform.rotation = endRotation;
            model.IsFlipped = !model.IsFlipped;
            model.FlipCardCoroutine = null;
            
            //Send event to check if the card is a match
            if(model.IsFlipped)GetController<CardEventController>().SendOnCardFlippedEvent();
        }

        #endregion
    }
}