using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Infrastructure.Source.MVC.Objects.Controllers;
using VContainer.Unity;

namespace Framework
{
    public class GameView : MonoBehaviour
    {
        #region Variables
        
        internal GameMethods _gameMethods;
        private Dictionary<Type, object> _rawModels;
        private Dictionary<Type, object> _rawControllers;
        private static GameDependency _dependency = null;
        private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        
        private const string INITIALIZE_METHOD = "Initialize";
        private const string AWAKE_METHOD = "Awake";
        private const string PRE_AWAKE_METHOD = "PreAwake";
        private const string SET_MODEL_METHOD = "SetModel";
        private const string SET_VIEW_METHOD = "SetView";
        private const string DESTROY_METHOD = "Destroy";
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        #endregion

        #region Lifecycle

        internal virtual void Awake()
        {
            try
            {
                _dependency = _dependency == null ? LifetimeScope.Find<GameDependency>().GetComponent<GameDependency>() : _dependency;
            }
            catch (Exception e)
            {
                Debug.LogWarning("[MVC] Error trying to find the GameDependency component. Please make sure you have a GameDependency component in the scene. Error: " + e.Message);
            }
            InitializeModelAttributes();
            InitializeControllerAttributes();
        }

        #endregion

        #region Internal Methods

        public void Initialize<T>(params object[] value)
        {
            var model = _rawModels[typeof(T)];
            var method = model.GetType().GetMethod(INITIALIZE_METHOD, BINDING_FLAGS);
            method?.Invoke(model, value);
        }

        internal void ReceiveEventFromChild<T>(string functionName, params object[] value)
        {
            var controller = _rawControllers[typeof(T)];
            var method = controller.GetType().GetMethod(functionName, BINDING_FLAGS);
            method?.Invoke(controller, value);
        }

        private void InitializeModelAttributes(bool force = false)
        {
            if (!Application.isPlaying && !force) return;
            Type modelType = null;
            try
            {
                _rawModels = new Dictionary<Type, object>();
                InitializeModelByType(GetType());
                _rawModels.ToList().ForEach(it =>
                {
                    modelType = it.Key;
                    var currentValue = ((GameModel)it.Value);
                    var callAwake = it.Key.GetMethod(AWAKE_METHOD, BINDING_FLAGS)!.MakeGenericMethod(it.Key);
                    callAwake?.Invoke(currentValue, null);
                });
            }
            catch (Exception e)
            {
                Debug.LogWarning("[MVC] " + e.Message + " | Current Class View: " + GetType().Name + " | Current Type Model: " + modelType?.Name + "\n");
                throw;
            }
        }

        private void InitializeModelByType(Type value)
        {
            var fields = new List<FieldInfo>();
            var currentType = value;
            while (currentType != null)
            {
                fields.AddRange(currentType.GetFields(BINDING_FLAGS));
                currentType = currentType.BaseType;
            }
            
            fields.ForEach(field =>
            {
                var attributes = field.GetCustomAttributes(typeof(GameFieldAttributes.ModelFieldAttribute), false);
                if (attributes.Length <= 0) return;
                var modelType = field.FieldType;
                var modelValue = field.GetValue(this);
                if (modelValue == null)
                {
                    modelValue = Activator.CreateInstance(modelType);
                    field.SetValue(this, modelValue);
                }
                _rawModels.Add(modelType, modelValue);
            });
        }

        private void InitializeControllerAttributes(bool force = false)
        {
            _gameMethods = new GameMethods();
            _rawControllers = new Dictionary<Type, object>();
            if (!Application.isPlaying && !force) return;

            //Get all the controllers
            var type = GetType();
            var fields = new List<FieldInfo>();
            var currentType = type;
            while (currentType != null)
            {
                fields.AddRange(currentType.GetFields(BINDING_FLAGS));
                currentType = currentType.BaseType;
            }
                
            fields.ForEach(field =>
            {
                var attributes = field.GetCustomAttributes(typeof(GameFieldAttributes.ControllerFieldAttribute), false);
                if (attributes.Length > 0)
                {
                    var controllerType = field.FieldType;
                    var controllerInstance = Activator.CreateInstance(controllerType, this);
                    field.SetValue(this, controllerInstance);
                    _dependency?.Inject(controllerInstance); //Inject dependencies into the controller
                    _rawControllers.Add(controllerType, controllerInstance);
                }
            });
            
            //Setup the controller
            foreach (var it in _rawControllers.Values)
            {
                //Find SetView and call
                var setViewMethod = it.GetType().GetMethod(SET_VIEW_METHOD, BINDING_FLAGS);
                var setViewParameters = new object[] { this };
                setViewMethod?.Invoke(it, setViewParameters);
                
                //Find SetModel and call
                foreach (var model in _rawModels)
                {
                    var setModelMethod = it.GetType().GetMethod(SET_MODEL_METHOD, BINDING_FLAGS)!.MakeGenericMethod(model.Key);
                    var setModelParameters = new object[] { model.Value };
                    setModelMethod?.Invoke(it, setModelParameters);
                }
            }

            //Get all the methods to bind
            var methodsToBindToController =
                Enum.GetValues(typeof(GameMethodEvents)).Cast<GameMethodEvents>().ToList();
            
            //Now bind the events methods
            foreach (var it in _rawControllers.Values)
            {
                methodsToBindToController.ForEach(methodToBind =>
                {
                    var newMethod = it.GetType().GetMethod(methodToBind.ToString(), BINDING_FLAGS);
                    if (newMethod != null)
                        _gameMethods.AddMethod(methodToBind, new GameMethod { MethodInfo = newMethod, AssociatedObject = it });
                });
            }
            
            //PreAwake Controller
            foreach (var it in _rawControllers.Values)
            {
                //Find Awake and call
                var invokeMethod = it.GetType().GetMethod(PRE_AWAKE_METHOD, BINDING_FLAGS);
                invokeMethod?.Invoke(it, null);
            }
            
            //Awake Controller
            foreach (var it in _rawControllers.Values)
            {
                var invokeMethod = it.GetType().GetMethod(AWAKE_METHOD, BINDING_FLAGS);
                invokeMethod?.Invoke(it, null);
            }
        }

        #endregion

        #region Getters

        public T GetModel<T>() where T : GameModel
        {
            var type = typeof(T);
            var model = _rawModels.TryGetValue(type, out var rawModel) ? (T) rawModel : throw new Exception("Model not found");
            return (T) model.Clone();
        }
        
        public T GetController<T>()
        {
            var type = typeof(T);
            var controller = _rawControllers.TryGetValue(type, out var rawController) ? (T) rawController : throw new Exception("Controller not found");
            return (T) controller;
        }
        
        public CancellationToken GetCancellationToken() => _cancellationToken.Token;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _gameMethods?.GetList(GameMethodEvents.Start).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void Update()
        {
            _gameMethods?.GetList(GameMethodEvents.Update).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void FixedUpdate()
        {
            _gameMethods?.GetList(GameMethodEvents.FixedUpdate).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void LateUpdate()
        {
            _gameMethods?.GetList(GameMethodEvents.LateUpdate).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void OnEnable()
        {
            _gameMethods?.GetList(GameMethodEvents.OnEnable).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void OnDisable()
        {
            _gameMethods?.GetList(GameMethodEvents.OnDisable).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void OnDestroy()
        {
            _gameMethods?.GetList(GameMethodEvents.OnDestroy).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
            _rawModels?.ToList().ForEach(it =>
            {
                var currentValue = ((GameModel)it.Value);
                var callAwake = it.Key.GetMethod(DESTROY_METHOD, BINDING_FLAGS)?.MakeGenericMethod(it.Key);
                callAwake?.Invoke(currentValue, null);
            });
            _cancellationToken.Cancel();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionEnter).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnCollisionExit(Collision other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionExit).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnCollisionStay(Collision other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionStay).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerEnter).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnTriggerExit(Collider other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerExit).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnTriggerStay(Collider other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerStay).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnMouseDown()
        {
            _gameMethods?.GetList(GameMethodEvents.OnMouseDown).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }
        
        private void OnMouseEnter()
        {
            _gameMethods?.GetList(GameMethodEvents.OnMouseEnter).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            }); 
        }
        
        private void OnMouseExit()
        {
            _gameMethods?.GetList(GameMethodEvents.OnMouseExit).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, null);
            });
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionEnter2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionExit2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnCollisionStay2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerEnter2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            }); 
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerExit2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnTriggerStay2D).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        #endregion
    }
}