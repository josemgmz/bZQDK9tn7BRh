using System.Collections;
using Framework;
using Game.Entities.UIMenu.Data;
using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.Entities.UIMenu
{
    public class UIMenuController :  GameController<UIMenuView>
    {
        #region Services

        [Inject] private IStorageService _storageService;
        [Inject] private IRoundService _roundService;
        [Inject] private IGameEventBus _gameEventBus;

        #endregion
        
        #region Lifecycle

        private void Awake()
        {
            var model = GetModel<UIMenuModel>();
            model.PlayButton.onClick.AddListener(OnPlayButtonClicked);
            model.ClearProgressButton.onClick.AddListener(OnClearProgressButtonClicked);
            _gameEventBus.AddListener<OnUIMenuEnableEvent>(OnUIMenuEnableEvent);
        }
        
        private void Start()
        {
            var model = GetModel<UIMenuModel>();
            model.Score.text = _storageService.GetHighestScore().ToString();
            model.LastRound.text = _storageService.GetLastRound().ToString();
            
        }
        
        private void OnDestroy()
        {
            var model = GetModel<UIMenuModel>();
            model.PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
            model.ClearProgressButton.onClick.RemoveListener(OnClearProgressButtonClicked);
            _gameEventBus.RemoveListener<OnUIMenuEnableEvent>(OnUIMenuEnableEvent);
        }

        #endregion
        
        #region Private Methods

        private void OnUIMenuEnableEvent(OnUIMenuEnableEvent eventData)
        {
            Start();
            StartCoroutine(ContainerAnimation(false));
            StartCoroutine(PanelAnimation(false));
        }

        private void OnPlayButtonClicked()
        {
            StartCoroutine(ContainerAnimation());
            StartCoroutine(PanelAnimation());
            if(!_roundService.RoundInProgress()) _roundService.StartRound();
        }
        
        private void OnClearProgressButtonClicked()
        {
            _storageService.ClearData();
            _roundService.SetRound(0);
            _roundService.EndRound(false);
            
            var model = GetModel<UIMenuModel>();
            model.Score.text = "0";
            model.LastRound.text = "0";
        }

        private IEnumerator ContainerAnimation(bool isOut = true)
        {
            var model = GetModel<UIMenuModel>();
            var content = model.Content;
            var startPosition = content.anchoredPosition;
            var endPosition = isOut ? new Vector2(startPosition.x, startPosition.y - content.rect.height) : new Vector2(startPosition.x, startPosition.y + content.rect.height);
            var duration = 0.50f; // Duration of the animation in seconds
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                content.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            content.anchoredPosition = endPosition;
        }
        
        private IEnumerator PanelAnimation(bool isOut = true)
        {
            var model = GetModel<UIMenuModel>();
            var leftPanel = model.LeftPanel;
            var rightPanel = model.RightPanel;

            var leftStartPosition = leftPanel.anchoredPosition;
            var rightStartPosition = rightPanel.anchoredPosition;

            var leftPanelWidth = isOut ? -leftPanel.rect.width : leftPanel.rect.width;
            var rightPanelWidth = isOut ? rightPanel.rect.width : -rightPanel.rect.width;
            var leftEndPosition = new Vector2(leftStartPosition.x + leftPanelWidth, leftStartPosition.y);
            var rightEndPosition = new Vector2(rightStartPosition.x + rightPanelWidth, rightStartPosition.y);

            var duration = 0.50f; // Duration of the animation in seconds
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                leftPanel.anchoredPosition = Vector2.Lerp(leftStartPosition, leftEndPosition, elapsedTime / duration);
                rightPanel.anchoredPosition = Vector2.Lerp(rightStartPosition, rightEndPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            leftPanel.anchoredPosition = leftEndPosition;
            rightPanel.anchoredPosition = rightEndPosition;
        }

        #endregion
    }
}