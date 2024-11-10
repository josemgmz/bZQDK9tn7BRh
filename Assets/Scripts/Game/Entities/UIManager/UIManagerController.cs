using Framework;
using Game.Entities.UIMenu.Data;
using Game.Entities.UITextElement;
using Game.Entities.UITextElement.Data;
using UnityEngine;
using VContainer;

namespace Game.Entities.UIManager
{
    /// <summary>
    /// Controls the UI manager, handling updates and interactions with UI elements.
    /// </summary>
    public class UIManagerController : GameController<UIManagerView>
    {
        #region Services

        /// <summary>
        /// Event bus for handling game events.
        /// </summary>
        [Inject] private IGameEventBus _gameEventBus;

        #endregion

        #region Lifecycle

        /// <summary>
        /// Initializes the UI manager controller.
        /// </summary>
        private void Start()
        {
            _gameEventBus.AddListener<OnUIManagerUpdateEvent>(OnUIManagerUpdateEvent);
            var model = GetModel<UIManagerModel>();
            model.TimeController = model.Time.GetController<UITextElementController>();
            model.TurnsController = model.Turns.GetController<UITextElementController>();
            model.ComboController = model.Combo.GetController<UITextElementController>();
            model.ScoreController = model.Score.GetController<UITextElementController>();
            model.SettingsButton.onClick.AddListener(RaiseUIMenuEnableEvent);
            model.StartTime = Time.time;
        }

        /// <summary>
        /// Updates the UI manager each frame.
        /// </summary>
        private void Update()
        {
            var model = GetModel<UIManagerModel>();
            var elapsedTime = Time.time - model.StartTime;
            model.TimeController.SetValue(elapsedTime.ToString("F2"));
        }

        /// <summary>
        /// Cleans up the UI manager controller.
        /// </summary>
        private void OnDestroy()
        {
            _gameEventBus.RemoveListener<OnUIManagerUpdateEvent>(OnUIManagerUpdateEvent);
            var model = GetModel<UIManagerModel>();
            model.SettingsButton.onClick.RemoveListener(RaiseUIMenuEnableEvent);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Raises the UI menu enable event.
        /// </summary>
        private void RaiseUIMenuEnableEvent()
        {
            _gameEventBus.RaiseEvent(new OnUIMenuEnableEvent{});
        }

        /// <summary>
        /// Handles the UI manager update event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
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