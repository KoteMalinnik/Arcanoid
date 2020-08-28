using UnityEngine;

public static class Directions
{
    static Vector2 ToLeftTop = new Vector2(-1, 1);
    static Vector2 ToRightTop = new Vector2(1, 1);
    static Vector2 ToLeftBottom = new Vector2(-1, -1);
    static Vector2 ToRightBottom = new Vector2(1, -1);

    public static Vector2 GetCorrectDirection(Vector2 direction)
    {
        Vector2 targetDirection = ToLeftTop;
        float distance = Vector2.Distance(direction, ToLeftTop);
        float minDistance = distance;

        distance = Vector2.Distance(direction, ToRightTop);
        if (minDistance > distance)
        {
            targetDirection = ToRightTop;
            minDistance = distance;
        }

        distance = Vector2.Distance(direction, ToLeftBottom);
        if (minDistance > distance)
        {
            targetDirection = ToLeftBottom;
            minDistance = distance;
        }

        distance = Vector2.Distance(direction, ToRightBottom);
        if (minDistance > distance)
        {
            targetDirection = ToRightBottom;
        }

        return targetDirection;
    }
}
