using VContainer.Unity;

namespace Framework
{
    public class GameDependency : LifetimeScope
    {
        #region Methods

        public void Inject(object instance)
        {
            Container.Inject(instance);
        }

        #endregion
    }
}