using UnityEngine;
using UnityEngine.UI;

namespace Game.Helper
{
    /// <summary>
    /// A custom grid layout group that adjusts its layout based on the size of the container and the reference sprite.
    /// </summary>
    [ExecuteInEditMode]
    public class ResponsiveGridLayout : GridLayoutGroup
    {
        #region Variables

        /// <summary>
        /// The reference sprite used to determine the size of the grid cells.
        /// </summary>
        public Sprite m_ReferenceSprite;

        /// <summary>
        /// The spacing between elements in the grid.
        /// </summary>
        public Vector2 m_ElementsSpacing;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the number of columns in the grid.
        /// </summary>
        /// <param name="columns">The number of columns.</param>
        public void SetColumns(int columns)
        {
            m_Constraint = Constraint.FixedColumnCount;
            m_ConstraintCount = columns;
        }

        /// <summary>
        /// Sets the spacing between elements in the grid.
        /// </summary>
        /// <param name="spacing">The spacing between elements.</param>
        public void SetSpacing(Vector2 spacing)
        {
            m_ElementsSpacing = spacing;
        }

        /// <summary>
        /// Calculates the horizontal layout input for the grid.
        /// </summary>
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalculateResponsiveLayout();
        }

        /// <summary>
        /// Calculates the vertical layout input for the grid.
        /// </summary>
        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputVertical();
            CalculateResponsiveLayout();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculates the responsive layout for the grid based on the container size and reference sprite.
        /// </summary>
        private void CalculateResponsiveLayout()
        {
            if (rectChildren.Count == 0) return;

            var minColumns = 0;
            var preferredColumns = 0;

            // Determine the number of columns based on the constraint type
            switch (m_Constraint)
            {
                case Constraint.FixedColumnCount:
                    minColumns = preferredColumns = m_ConstraintCount;
                    break;
                case Constraint.FixedRowCount:
                    minColumns = preferredColumns = Mathf.CeilToInt(rectChildren.Count / (float)m_ConstraintCount - 0.001f);
                    break;
                default:
                    minColumns = 1;
                    preferredColumns = Mathf.CeilToInt(Mathf.Sqrt(rectChildren.Count));
                    break;
            }

            // Choose the greater value between minColumns and preferredColumns
            var columns = minColumns >= preferredColumns ? minColumns : preferredColumns;
            // Calculate the number of rows needed
            var rows = Mathf.CeilToInt(rectChildren.Count / (float)columns);

            // Get the dimensions of the container
            var containerWidth = rectTransform.rect.width;
            var containerHeight = rectTransform.rect.height;

            // Get the dimensions of the reference sprite
            var childWidth = m_ReferenceSprite.rect.width;
            var childHeight = m_ReferenceSprite.rect.height;

            // Calculate the total width and height required for all children
            var childWidthFull = childWidth * columns;
            var childHeightFull = childHeight * rows;
            childWidthFull += padding.horizontal + ((columns) * spacing.x);
            childHeightFull += padding.vertical + ((rows) * spacing.y);

            // Calculate the ratio to scale the children to fit within the container
            var widthRatio = childWidthFull / containerWidth;
            var heightRatio = childHeightFull / containerHeight;
            var ratio = widthRatio > heightRatio ? widthRatio : heightRatio;

            // Calculate the width and height of each cell after scaling
            var cellWidth = (childWidth) / ratio;
            var cellHeight = (childHeight) / ratio;

            // Adjust the cell size to account for element spacing
            cellWidth -= (m_ElementsSpacing.x / ratio);
            cellHeight -= (m_ElementsSpacing.y / ratio);

            // Set the cell size and spacing for the grid layout
            cellSize = new Vector2(cellWidth, cellHeight);
            spacing = new Vector2(m_ElementsSpacing.x / ratio, m_ElementsSpacing.y / ratio);
        }

        #endregion
    }
}