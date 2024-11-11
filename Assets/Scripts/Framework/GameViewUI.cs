using UnityEngine.EventSystems;

namespace Framework
{
    /// <summary>
    /// Represents the UI view component in the MVC pattern, handling various pointer and drag events.
    /// </summary>
    public class GameViewUI : GameView, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerMoveHandler, IPointerEnterHandler
    {
        #region Unity Methods

        public void OnBeginDrag(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnBeginDrag).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        public void OnDrag(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnDrag).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        public void OnEndDrag(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnEndDrag).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            }); 
        }
        
        public void OnPointerDown(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnPointerDown).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        public void OnPointerExit(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnPointerExit).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        public void OnPointerMove(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnPointerMove).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            }); 
        }
        
        public void OnPointerUp(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnPointerUp).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }
        
        public void OnPointerEnter(PointerEventData other)
        {
            _gameMethods?.GetList(GameMethodEvents.OnPointerEnter).ForEach(it =>
            {
                it.MethodInfo.Invoke(it.AssociatedObject, new object[] {other});
            });
        }

        #endregion
    }
}