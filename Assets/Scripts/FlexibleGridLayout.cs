using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [Header("Flexible Grid")]
    public FitType fitType = FitType.Uniform;

    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            float squareRoot = Mathf.Sqrt(rectChildren.Count);
            rows = columns = Mathf.CeilToInt(squareRoot);
            switch (fitType)
            {
                case FitType.Width:
                    fitX = true;
                    fitY = false;
                    break;
                case FitType.Height:
                    fitX = false;
                    fitY = true;
                    break;
                case FitType.Uniform:
                    fitX = fitY = true;
                    break;
            }
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(rectChildren.Count / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(rectChildren.Count / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float totalSpacingX = spacing.x * (columns - 1);
        float totalSpacingY = spacing.y * (rows - 1);

        float cellWidth = (parentWidth - padding.left - padding.right - totalSpacingX) / columns;
        float cellHeight = (parentHeight - padding.top - padding.bottom - totalSpacingY) / rows;

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;
    }

    public override void CalculateLayoutInputVertical()
    {
        // Không cần triển khai chi tiết ở đây
    }

    public override void SetLayoutHorizontal()
    {
        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];
            float rowSpan = item.GetComponent<LayoutElement>()?.flexibleHeight ?? 1; // Số lượng dòng mà phần tử chiếm
            float colSpan = item.GetComponent<LayoutElement>()?.flexibleWidth ?? 1;  // Số lượng cột mà phần tử chiếm

            int columnCount = i % columns;

            float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float itemWidth = cellSize.x * colSpan + spacing.x * (colSpan - 1);
            SetChildAlongAxis(item, 0, xPos, itemWidth);
        }
    }

    public override void SetLayoutVertical()
    {
        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];
            float rowSpan = item.GetComponent<LayoutElement>()?.flexibleHeight ?? 1; // Số lượng dòng mà phần tử chiếm
            float colSpan = item.GetComponent<LayoutElement>()?.flexibleWidth ?? 1;  // Số lượng cột mà phần tử chiếm

            int rowCount = i / columns;

            float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
            float itemHeight = cellSize.y * rowSpan + spacing.y * (rowSpan - 1);
            SetChildAlongAxis(item, 1, yPos, itemHeight);
        }
    }
}
