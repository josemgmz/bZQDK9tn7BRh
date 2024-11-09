
using System.Collections;
using Framework;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.Tests.TestFramework
{
    public abstract class TestFramework : InputTestFixture
    {
        #region Variables
        
        private Touchscreen _touchscreen;
        private GameAddressableContext _addressableContext = new();

        #endregion

        #region Initialization

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _touchscreen = InputSystem.AddDevice<Touchscreen>();
            var scenePath = $"Assets/Scripts/Game/Tests/E2ETest/{GetTestDirectoryName()}/Test.unity";
            EditorSceneManager.LoadSceneInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Single));
        }

        #endregion
        
        #region Getter

        protected abstract string GetTestDirectoryName();

        #endregion
        
        #region Input

        public void StartTouch(Vector3 position, RenderMode renderMode = RenderMode.ScreenSpaceOverlay, int index = 0)
        {
            var startPosition = ConvertToScreenPosition(position, renderMode);
            DrawDebugLines(renderMode != RenderMode.ScreenSpaceCamera ? startPosition : position);
            BeginTouch(index, startPosition, screen: GetTouchscreen());
        }

        public void UpdateTouch(Vector3 position, RenderMode renderMode = RenderMode.ScreenSpaceOverlay, int index = 0)
        {
            var currentPosition = ConvertToScreenPosition(position, renderMode);
            DrawDebugLines(renderMode != RenderMode.ScreenSpaceCamera ? currentPosition : position);
            MoveTouch(index, currentPosition, screen: GetTouchscreen());
        }

        public void StopTouch(Vector3 position, RenderMode renderMode = RenderMode.ScreenSpaceOverlay, int index = 0)
        {
            EndTouch(index, ConvertToScreenPosition(position, renderMode), screen: GetTouchscreen());
        }

        public IEnumerator PerformDrag(Vector3 initialPosition, Vector3 targetPosition, float duration, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            StartTouch(initialPosition, renderMode);
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);
                UpdateTouch(newPosition, renderMode);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StopTouch(targetPosition, renderMode);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        private Vector3 ConvertToScreenPosition(Vector3 input, RenderMode renderMode = RenderMode.ScreenSpaceOverlay)
        {
            return renderMode != RenderMode.ScreenSpaceOverlay ? Camera.main!.WorldToScreenPoint(input) : input;
        }

        private void DrawDebugLines(Vector3 position)
        {
            Debug.DrawLine(position, position + Vector3.up * 25, Color.magenta, 25);
            Debug.DrawLine(position, position + Vector3.right * 25, Color.magenta, 25);
            Debug.DrawLine(position, position + Vector3.up * -25, Color.magenta, 25);
            Debug.DrawLine(position, position + Vector3.right * -25, Color.magenta, 25);
        }

        protected Touchscreen GetTouchscreen() => _touchscreen;

        #endregion

        #region Methods

        public TObject LoadAsset<TObject>(string key) where TObject : Object
        {
            return _addressableContext.LoadAsset<TObject>(key);
        }

        #endregion

        #region Deinitialization

        [TearDown]
        public override void TearDown()
        {
            Cleanup();
            base.TearDown();
        }

        private void Cleanup()
        {
            InputSystem.ResetDevice(_touchscreen);
            InputSystem.Update();
            InputSystem.RemoveDevice(_touchscreen);
            InputSystem.Update();
            _addressableContext.Release();
            _touchscreen = null;
        }

        #endregion
    }
}