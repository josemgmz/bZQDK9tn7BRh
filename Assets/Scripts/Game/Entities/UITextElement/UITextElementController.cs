using Framework;

namespace Game.Entities.UITextElement
{
    /// <summary>
    /// Controls the UI text element by setting its title and value.
    /// </summary>
    public class UITextElementController : GameController<UITextElementView>
    {
        #region Public Methods

        /// <summary>
        /// Sets the title of the UI text element.
        /// </summary>
        /// <param name="title">The title to set.</param>
        public void SetTitle(string title)
        {
            GetModel<UITextElementModel>().Title.text = title;
        }

        /// <summary>
        /// Sets the value of the UI text element.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetValue(string value)
        {
            GetModel<UITextElementModel>().Value.text = value;
        }

        #endregion
    }
}