using System.Collections;
using Framework;
using Game.Helper;
using Game.Services;
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
            model.Image.sprite = model.BackSprite;
        }

        #endregion
        
        #region Events

        private void OnPointerUp(PointerEventData other)
        {
            var model = GetModel<CardModel>();
            if(model.FlipCardCoroutine != null) return;
            model.FlipCardCoroutine = StartCoroutine(FlipCard());
        }

        #endregion

        #region Methods

        private IEnumerator FlipCard()
        {
            _audioService.PlaySfx(CardModel.CARD_FLIP_SFX);
            var model = GetModel<CardModel>();
            var duration = model.RotationTime;
            var startRotation = transform.rotation;
            var midRotation = startRotation * Quaternion.Euler(0, -90, 0);
            var endRotation = startRotation * Quaternion.Euler(0, -180, 0);

            yield return transform.RotateTo(startRotation, midRotation, duration / 2);

            // Set rotation to exactly 90 degrees and change sprite
            transform.rotation = midRotation;
            model.Image.sprite = model.Image.sprite == model.BackSprite ? model.FrontSprite : model.BackSprite;

            yield return transform.RotateTo(midRotation, endRotation, duration / 2);

            // Set rotation to exactly 180 degrees
            transform.rotation = endRotation;
            model.IsFlipped = !model.IsFlipped;
            model.FlipCardCoroutine = null;
        }

        #endregion
    }
}