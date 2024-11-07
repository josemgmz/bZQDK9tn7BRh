

using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Test
{
    [TestFixture]
    public class ExecutionOrderTest
    {

        #region Test

        [Test(ExpectedResult = (IEnumerator)null)]
        public IEnumerator  ExecutionOrder()
        {
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<ObjectView>();
            yield return new WaitForSeconds(1);
        }

        #endregion

        #region Classes

        [Serializable]
        public class ObjectModel : GameModel
        {
            public long firstFrame;
            public long currentStepCounter = 1;
        }

        public class ObjectView : GameView
        {
            #region Variables

            [GameFieldAttributes.ModelField, SerializeField] private ObjectModel _model;
            [GameFieldAttributes.ControllerField] private ObjectController _controller;

            #endregion
        }
        
        public class ObjectController : GameController<ObjectView>
        {
            #region Lifecycle

            private void Awake()
            {
                Debug.Log("Awake - 1");
                var model = GetModel<ObjectModel>();
                model.firstFrame = Time.frameCount;
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 1);
            }
            
            private void OnEnable()
            {
                Debug.Log("OnEnable - 2");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 2);
            }
            
            private void Start()
            {
                Debug.Log("Start - 3");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 3);
            }
            
            private void Update()
            {
                Debug.Log("Update - 4");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 4);
            }
            
            private void LateUpdate()
            {
                var model = GetModel<ObjectModel>();
                // Skip first frame because unity calls this LateUpdate before Update on the first frame
                if (model.firstFrame == Time.frameCount) return; 
                Debug.Log("LateUpdate - 5");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 5);
                Destroy();
            }
            
            private void OnDisable()
            {
                Debug.Log("OnDisable - 6");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 6);
            }
            
            private void OnDestroy()
            {
                Debug.Log("OnDestroy - 7");
                Assert.AreEqual(GetModel<ObjectModel>().currentStepCounter++, 7);
            }
            

            #endregion
        }
        

        #endregion
    }
}
