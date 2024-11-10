using Framework;

namespace Game.Entities.UITextElement
{
    public class UITextElementController : GameController<UITextElementView>
    {
        #region Public Methods
        
        public void SetTitle(string title)
        {
            GetModel<UITextElementModel>().Title.text = title;
        }
        
        public void SetValue(string value)
        {
            GetModel<UITextElementModel>().Value.text = value;
        }
        
        #endregion
    }
}