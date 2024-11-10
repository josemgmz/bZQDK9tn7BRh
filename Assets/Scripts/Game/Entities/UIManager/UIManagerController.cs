using Framework;
using Game.Entities.UITextElement;
using Game.Entities.UITextElement.Data;
using UnityEngine;
using VContainer;

namespace Game.Entities.UIManager
{
    public class UIManagerController : GameController<UIManagerView>
    {
        #region Services

        [Inject] private IGameEventBus _gameEventBus;

        #endregion
        
        #region Lifecycle

        private void Start()
        {
            _gameEventBus.AddListener<OnUIManagerUpdateEvent>(OnUIManagerUpdateEvent);
            var model = GetModel<UIManagerModel>();
            model.TimeController = model.Time.GetController<UITextElementController>();
            model.TurnsController = model.Turns.GetController<UITextElementController>();
            model.ComboController = model.Combo.GetController<UITextElementController>();
            model.ScoreController = model.Score.GetController<UITextElementController>();
            model.StartTime = Time.time;
        }

        private void Update()
        {
            var model = GetModel<UIManagerModel>();
            var elapsedTime = Time.time - model.StartTime;
            model.TimeController.SetValue(elapsedTime.ToString("F2"));
        }
        
        private void OnDestroy()
        {
            _gameEventBus.RemoveListener<OnUIManagerUpdateEvent>(OnUIManagerUpdateEvent);
        }

        #endregion

        #region Private Methods

        private void OnUIManagerUpdateEvent(OnUIManagerUpdateEvent eventData)
        {
            var model = GetModel<UIManagerModel>();
            if(eventData.ResetTime) model.StartTime = Time.time;
            model.TurnsController.SetValue(eventData.Turns.ToString());
            model.ComboController.SetValue(eventData.Combo.ToString());
            model.ScoreController.SetValue(eventData.Score.ToString());
        }

        #endregion
    }
}