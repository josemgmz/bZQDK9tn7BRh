using System;

namespace Framework
{
    public class GameFieldAttributes
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class ControllerFieldAttribute : System.Attribute {}
        
        [AttributeUsage(AttributeTargets.Field)]
        public class ModelFieldAttribute : System.Attribute {}
    }
}