using System;
using System.Collections;

namespace Framework
{
    public interface IGameEventBus
    {
        void AddListener<T>(Action<T> listener, GameController<GameView> owner = null);
        void AddListener<T>(Action listener, GameController<GameView> owner = null);
        void AddListener(Action<Type> listener, Type type);
        void AddListener(Action listener, Type type);
        void AddListener<T>(Func<IEnumerator> listener, GameController<GameView> owner = null);
        void AddListener<T>(Func<T,IEnumerator> listener, GameController<GameView> owner = null);
        
        void RemoveListener<T>(Action<T> listener);
        void RemoveListener<T>(Action listener);
        void RemoveListener(Action listener, Type type);
        void RemoveListener<T>(Func<IEnumerator> listener);
        void RemoveListener<T>(Func<T, IEnumerator> listener);

        void RaiseEvent<T>(T args = null) where T : class;
    }
}