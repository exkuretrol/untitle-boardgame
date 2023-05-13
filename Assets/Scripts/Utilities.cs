using UnityEngine;
public class Utilities
{
    public static Vector3Int OffsetToCube(Vector2Int offset)
    {
        var q = offset.x;
        var r = offset.y - (offset.x - (offset.x % 2)) / 2;
        return new Vector3Int(q, r, - q - r);
    }
    
    public static Vector3 GetPositionForHexFromCoordinate(int column, int row, bool isFlatTopped = true, float radius = 1f)
    {
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        var size = radius;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (column * (horizontalDistance)) + offset;
            yPosition = (row * verticalDistance);
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3f) * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;
            xPosition = (column * (horizontalDistance));
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, 0, - yPosition);
    }
}