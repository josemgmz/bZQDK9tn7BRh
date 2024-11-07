using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    
    public abstract class GameController<TView> where TView : GameView
    {
        #region Properties
        
        private TView _view = null;
        private Dictionary<Type, object> _models;

        protected Transform transform
        {
            get => _view.transform;
            set => _view.transform.SetPositionAndRotation(value.position, value.rotation);
        }

        protected GameObject gameObject
        {
            get => _view.gameObject;
        }
        
        protected RectTransform rectTransform
        {
            get => _view.GetComponent<RectTransform>();
        }

        #endregion

        #region Getters and Setters
        
        

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _view.StartCoroutine(coroutine);
        }
        
        protected GameObject Instantiate(GameObject value)
        {
            return GameObject.Instantiate(value);
        }
        
        protected GameObject Instantiate(GameObject value, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(value, position, rotation);
        }
        
        protected GameObject Instantiate(GameObject value, Vector3 position, Quaternion rotation, Transform parent)
        {
            return GameObject.Instantiate(value, position, rotation, parent);
        }
        
        protected GameObject Instantiate(GameObject value, Transform parent)
        {
            return GameObject.Instantiate(value, parent);
        }
        
        
        protected T GetComponent<T>()
        {
            return _view.GetComponent<T>();
        }
        
        public void SetView(TView view)
        {
            if (_view != null) throw new Exception("View already exist, can't set new view");
            _view = view;
        }

        public void SetModel<T>(object model)
        {
            var type = typeof(T);
            _models ??= new Dictionary<Type, object>();
            if (!_models.ContainsKey(type))
            {
                _models.Add(type, model);
                return;
            }
            throw new Exception("Model already exist, can't set new model with this type");
        }

        protected T GetModel<T>()
        {
            var type = typeof(T);
            return _models.ContainsKey(type) ? (T) _models[type] : throw new Exception("Model not found");
        }
        
        public TView GetView() => _view;

        protected TControllerType GetModelFromOtherObject<TControllerType>(GameObject value) where TControllerType : GameModel
        {
            var componentExist = value.TryGetComponent(out GameView viewComponent);
            if(!componentExist) throw new Exception("GameView component not found");
            return viewComponent.GetModel<TControllerType>();
        }

        protected ControllerType GetController<ControllerType>()
        {
            return GetControllerFromOtherObject<ControllerType>(gameObject);
        }
        protected TControllerType GetControllerFromOtherObject<TControllerType>(GameObject value)
        {
            var componentExist = value.TryGetComponent(out GameView viewComponent);
            if(!componentExist) throw new Exception("GameView component not found");
            return viewComponent.GetController<TControllerType>();
        }

        protected List<TControllerType> GetChildControllers<TControllerType>()
        {
            var controllers = new List<TControllerType>();
            var transform = GetView().transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = transform.GetChild(i);
                var childGameObject = child.gameObject;
                var childViewExists = childGameObject.TryGetComponent(out GameView viewComponent);
                if(!childViewExists) continue;
                try
                {
                    var childController = viewComponent.GetController<TControllerType>();
                    controllers.Add(childController);
                }
                catch { /*ignored*/ }
            }
            return controllers;
        }

        protected List<TModelType> GetChildModels<TModelType>() where TModelType : GameModel
        {
            var models = new List<TModelType>();
            var transform = GetView().transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = transform.GetChild(i);
                models.Add(GetModelFromOtherObject<TModelType>(child.gameObject));
            }
            return models;
        }
        
        protected TControllerType GetControllerFromParent<TControllerType>()
        {
            var componentExist = GetView().transform.parent.TryGetComponent(out GameView viewComponent);
            if(!componentExist) throw new Exception("GameView component not found");
            return viewComponent.GetController<TControllerType>();
        }
        
        protected TModelType GetModelFromParent<TModelType>() where TModelType : GameModel
        {
            var componentExist = GetView().transform.parent.TryGetComponent(out GameView viewComponent);
            if(!componentExist) throw new Exception("GameView component not found");
            return viewComponent.GetModel<TModelType>();
        }
        
        protected void Destroy(Object value = null)
        {
            if (value == null)
            {
                Object.Destroy(_view);
                return;
            }
            Object.Destroy(value);
        }

        /*
        protected void SendEventToParent<ControllerType>(string functionName, params object[] data)
        {
            var componentExist = GetView().transform.parent.TryGetComponent(out GameView gameView);
            if(!componentExist) throw new Exception("GameView component not found");
            gameView.ReceiveEventFromChild<ControllerType>(functionName, data);
        }*/

        #endregion
    }
}