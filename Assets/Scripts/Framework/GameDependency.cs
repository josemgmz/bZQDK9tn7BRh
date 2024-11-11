using VContainer.Unity;

namespace Framework
{
    /// <summary>
    /// Manages the dependency injection for the game using VContainer.
    /// </summary>
    public class GameDependency : LifetimeScope
    {
        #region Methods

        /// <summary>
        /// Injects dependencies into the specified instance.
        /// </summary>
        /// <param name="instance">The instance to inject dependencies into.</param>
        public void Inject(object instance)
        {
            Container.Inject(instance);
        }

        #endregion
    }
}