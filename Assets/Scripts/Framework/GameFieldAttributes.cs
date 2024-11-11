using System;

namespace Framework
{
    /// <summary>
    /// Contains custom attributes for game fields.
    /// </summary>
    public class GameFieldAttributes
    {
        /// <summary>
        /// Attribute to mark a field as a controller field.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class ControllerFieldAttribute : System.Attribute {}

        /// <summary>
        /// Attribute to mark a field as a model field.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class ModelFieldAttribute : System.Attribute {}
    }
}