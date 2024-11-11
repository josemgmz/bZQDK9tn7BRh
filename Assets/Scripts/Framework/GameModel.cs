using System;

namespace Framework
{
    /// <summary>
    /// Represents the base class for game models, providing lifecycle management and cloning capabilities.
    /// </summary>
    public abstract class GameModel : ICloneable
    {
        #region Lifecycle

        public void Awake<T>() where T: GameModel
        {
        }
        
        public virtual void Destroy<T>()  where T: GameModel
        {
        }

        internal T GetType<T>() where T: GameModel
        {
            return (T) this;
        }

        #endregion

        #region Methods

        public object Clone() => MemberwiseClone();

        #endregion

    }
}