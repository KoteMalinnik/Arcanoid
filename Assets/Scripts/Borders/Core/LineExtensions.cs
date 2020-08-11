using UnityEngine;

public static class LineExtensions
{
    /// <summary>
    /// Вернет true, если отрезки [borderStart, borderEnd] и [rayStart, rayEnd] пересекаются. Координаты точки пересечения содержаться в intersectionPoint.
    /// </summary>
    public static bool LinesIntersection(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End, out Vector2 intersectionPoint)
    {
        //не рассматривается пограничный случай, когда отрезок (startPos2, endPos2) проходит через концы первого отрезка

        var v1 = (line2End.x - line2Start.x) * (line1Start.y - line2Start.y) - (line2End.y - line2Start.y) * (line1Start.x - line2Start.x);
        var v2 = (line2End.x - line2Start.x) * (line1End.y - line2Start.y) - (line2End.y - line2Start.y) * (line1End.x - line2Start.x);
        var v3 = (line1End.x - line1Start.x) * (line2Start.y - line1Start.y) - (line1End.y - line1Start.y) * (line2Start.x - line1Start.x);
        var v4 = (line1End.x - line1Start.x) * (line2End.y - line1Start.y) - (line1End.y - line1Start.y) * (line2End.x - line1Start.x);

        intersectionPoint = Vector2.zero;

        if (v1 * v2 < 0 && v3 * v4 < 0)
        {
            var line1Coefficients = LineCoefficients(line1Start, line1End);
            var line2Coefficients = LineCoefficients(line2Start, line2End);

            intersectionPoint = IntersectionPoint(line1Coefficients, line2Coefficients);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Вернет коэффициенты прямой Ax + By + C = 0, построенной по отрезку [startPoint, endPoint] в виде Vector3(A, B, C)
    /// </summary>
    static Vector3 LineCoefficients(Vector2 startPoint, Vector2 endPoint)
    {
        // Ax + By + C = 0
        Vector3 result = new Vector3();
        result.x = endPoint.y - startPoint.y;
        result.y = startPoint.x - endPoint.x;
        result.z = -startPoint.x * (endPoint.y - startPoint.y) + startPoint.y * (endPoint.x - startPoint.x);
        return result;
    }

    /// <summary>
    /// Вернет точку пересечения двух прямых, задаваемых с помощью коэффициентов A, B, C.
    /// </summary>
    static Vector2 IntersectionPoint(Vector3 coefficients1, Vector3 coefficients2)
    {
        float d = coefficients1.x * coefficients2.y - coefficients1.y * coefficients2.x;
        float dx = -coefficients1.z * coefficients2.y + coefficients1.y * coefficients2.z;
        float dy = -coefficients1.x * coefficients2.z + coefficients1.z * coefficients2.x;

        Vector2 intersectionPoint = new Vector2(dx / d, dy / d);

        return intersectionPoint;
    }
}
