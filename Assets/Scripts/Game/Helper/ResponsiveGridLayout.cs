using UnityEngine;
using UnityEngine.UI;

namespace Game.Helper
{
    [ExecuteInEditMode]
    public class ResponsiveGridLayout : GridLayoutGroup
    {
        #region Variables

        public Sprite m_ReferenceSprite;
        public Vector2 m_ElementsSpacing;

        #endregion

        #region Public Methods

        public void SetColumns(int columns)
        {
            m_Constraint = Constraint.FixedColumnCount;
            m_ConstraintCount = columns;
        }
        
        public void SetSpacing(Vector2 spacing)
        {
            m_ElementsSpacing = spacing;
        }
        
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalculateResponsiveLayout();
        }
        
        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputVertical();
            CalculateResponsiveLayout();
        }

        #endregion

        #region Private Methods

        private void CalculateResponsiveLayout()
        {
            if(rectChildren.Count == 0) return;

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