using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework
{
    public class GameMethod
    {
        public MethodInfo MethodInfo { get; set; }
        public object AssociatedObject { get; set; }
    }
    
    public class GameMethods
    {
        #region Properties
        
        private Dictionary<GameMethodEvents, List<GameMethod>> _methods;

        #endregion

        #region Initialization

        public GameMethods()
        {
            _methods = new Dictionary<GameMethodEvents, List<GameMethod>>();
            var methods =
                Enum.GetValues(typeof(GameMethodEvents)).Cast<GameMethodEvents>().ToList();
            
            methods.ForEach(it => _methods.Add(it, new List<GameMethod>()));
        }

        #endregion

        #region Methods

        public List<GameMethod> GetList(GameMethodEvents value)
        {
            return _methods[value];
        }

        public void AddMethod(GameMethodEvents key, GameMethod value)
        {
            _methods[key].Add(value);
        }

        #endregion
    }
}