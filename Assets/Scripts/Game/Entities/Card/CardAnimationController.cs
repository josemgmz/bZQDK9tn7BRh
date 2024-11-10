using System.Collections;
using Framework;
using Game.Helper;
using Game.Services;
using Game.Services.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Game.Entities.Card
{
    /// <summary>
    /// Controls the card animations, handling the flip and match animations.
    /// </summary>
    public class CardAnimationController : GameController<CardView>
    {
        #region Services

        /// <summary>
        /// Service for managing audio playback.
        /// </summary>
        [Inject] private IAudioService _audioService;

        #endregion

        #region Lifecycle

        /// <summary>
        /// Initializes the card animation controller.
        /// </summary>
        private void Start()
        {
            // Setup the face of the card
            var model = GetModel<CardModel>();
            if (model.IsFlipped) return;
            transform.rotation *= Quaternion.Euler(0, -180, 0);
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the pointer up event to flip the card.
        /// </summary>
        /// <param name="other">The pointer event data.</param>
        private void OnPointerUp(PointerEventData other)
        {
            var model = GetModel<CardModel>();
            if (model.IsFlipped) return;
            FlipCard();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Flips the card with an optional sound effect.
        /// </summary>
        /// <param name="playSound">Whether to play a sound effect.</param>
        public void FlipCard(bool playSound = true)
        {
            var model = GetModel<CardModel>();
            if (model.FlipCardCoroutine != null) return;
            model.FlipCardCoroutine = StartCoroutine(_FlipCard(playSound));
        }

        /// <summary>
        /// Matches the card with an animation.
        /// </summary>
        public void MatchCard()
        {
            StartCoroutine(_MatchCard());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Coroutine to flip the card with an animation.
        /// </summary>
        /// <param name="playSound">Whether to play a sound effect.</param>
        /// <returns>An enumerator for the animation.</returns>
        private IEnumerator _FlipCard(bool playSound)
        {
            if (playSound) _audioService.PlaySfx(AudioData.Sfx.CardFlip);
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

            // Send event to check if the card is a match
            if (model.IsFlipped) GetController<CardEventController>().SendOnCardFlippedEvent();
        }

        /// <summary>
        /// Coroutine to match the card with an animation.
        /// </summary>
        /// <returns>An enumerator for the animation.</returns>
        private IEnumerator _MatchCard()
        {
            var model = GetModel<CardModel>();
            var duration = model.ScaleTime;
            var startScale = transform.localScale;
            var midScale = startScale * 1.1f; // 10% larger
            var endScale = Vector3.zero;

            float elapsedTime = 0;
            while (elapsedTime < duration / 4)
            {
                transform.localScale = Vector3.Lerp(startScale, midScale, elapsedTime / (duration / 4));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;
            while (elapsedTime < (3 * duration) / 4)
            {
                transform.localScale = Vector3.Lerp(midScale, endScale, elapsedTime / ((3 * duration) / 4));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endScale;
        }

        #endregion
    }
}