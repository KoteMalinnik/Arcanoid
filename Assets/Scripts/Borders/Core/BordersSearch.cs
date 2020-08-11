using UnityEngine;
using System.Collections.Generic;
using System;

public static class BordersSearch
{
    static List<StaticBorder> borders = null; 

    static void Initialize()
    {
        Log.Message("Инициализация списка рамок.");
        borders = new List<StaticBorder>();
        borders.AddRange(MonoBehaviour.FindObjectsOfType<StaticBorder>());
        Log.Message("Рамок найдено: " + borders.Count);
    }

    //аналог Raycast(Ray ray, out Hit hit)
    public static bool CollisionAtDirection(
        StaticBorder sender,
        Vector2 fromPoint,
        Vector2 direction,
        out StaticBorder target,
        out Vector2 intersectionPoint,
        float duration = 5)
    {
        if (borders == null) Initialize();

        Log.Message($"Поиск рамок из позиции {fromPoint} в направлении {direction}.");

        direction.Normalize();
        if (direction.Equals(Vector2.zero)) throw new ArgumentOutOfRangeException();
        direction *= 100; //игровая область небольшая

        Vector2 endPoint = fromPoint + direction;

#if UNITY_EDITOR
        Debug.DrawLine(fromPoint, endPoint, Color.red, duration);
#endif


        /*
         * Алгоритм поиска пересечений
         * 1. Отбросить все координаты по X и Y осям, которые не надо рассматривать в направлении direction
         * 2. Проходить по каждой стороне рамки и смотреть, есть ли пересечение с отрезком direction.
         *      Если нет, то переходить к следующей рамке
         * 3. Сохранять только ближайший к точке fromPoint объект со StaticBorder
         */

        target = null;
        float distanceFromPointToTarget = 0;
        intersectionPoint = Vector2.zero;

        for (int i = 0; i < borders.Count; i++)
        {
            if (sender == borders[i]) continue; //на случай, если луч будет пересекать собственную рамку

            if (CheckBorder(borders[i], fromPoint, direction, out intersectionPoint))
            {
                if (target == null)
                {
                    target = borders[i];
                    Log.Message("Установка ближайшей рамки: " + target.name);
                    distanceFromPointToTarget = Vector3.Distance(fromPoint, intersectionPoint);
                    continue;
                }
                
                if (distanceFromPointToTarget < Vector3.Distance(target.transform.position, intersectionPoint))
                {
                    target = borders[i];
                    Log.Message("Установка ближайшей рамки: " + target.name);
                }
            }
        }

        Log.Message("Поиск рамки завершен.");

        if (target == null)
        {
            Log.Message("Рамок не найдено.");
            return false;
        }

        Log.Message($"Ближайший объект: {target.name}. Точка коллизии: {intersectionPoint}.");
        return true;
    }

    static bool CheckBorder(StaticBorder checkingStaticBorder, Vector2 rayStart, Vector2 rayEnd, out Vector2 intersectionPoint)
    {
        Log.Message($"Проверка рамки {checkingStaticBorder.name}");

        var border = checkingStaticBorder.border;
        //смотрим, находится ли рамка border хотя бы частично в прямоугольнике (fromPoint, directionRay)
        Rect directionalRect = new Rect(rayStart, rayEnd);
        Rect borderRect = new Rect(border.Left, border.Bottom, border.Right - border.Left, border.Top - border.Bottom);

        if (!directionalRect.Overlaps(borderRect))
        {
            Log.Message($"Рамка {checkingStaticBorder.name}  не находится в области поиска.");
            intersectionPoint = Vector2.zero;
            return false;
        }
            
        Log.Message($"Рамка {checkingStaticBorder.name} находится в области поиска.");
        return RayIntersectionWithBorder(border, rayStart, rayEnd, out intersectionPoint);
    }

    static bool RayIntersectionWithBorder(BorderData border, Vector2 rayStart, Vector2 rayEnd, out Vector2 intersectionPoint)
    {
        Log.Message("Проверка пересечения с гранями.");

        bool intersection = false;
        intersectionPoint = Vector2.zero;
        
        Vector2 tempIntersectionPoint = Vector2.zero;
        float minDistance = Vector2.Distance(rayStart, rayEnd);

        Vector2 leftBottomPoint = new Vector2(border.Left, border.Bottom);
        Vector2 leftTopPoint = border.TopLeftPoint;
        Vector2 rightBottomPoint = border.BottomRightPoint;
        Vector2 rightTopPoint = new Vector2(border.Right, border.Top);

        if (LineExtensions.LinesIntersection(leftTopPoint, leftBottomPoint, rayStart, rayEnd, out tempIntersectionPoint))
        {
            Log.Message("Перечесение с левой гранью в точке " + tempIntersectionPoint);
            intersectionPoint = NearestPoint(intersectionPoint, tempIntersectionPoint, rayStart, ref minDistance);
            intersection = true;
        }

        if (LineExtensions.LinesIntersection(leftBottomPoint, rightBottomPoint, rayStart, rayEnd, out tempIntersectionPoint))
        {
            Log.Message("Перечесение с нижней гранью в точке " + tempIntersectionPoint);
            intersectionPoint = NearestPoint(intersectionPoint, tempIntersectionPoint, rayStart, ref minDistance);
            intersection = true;
        }

        if (LineExtensions.LinesIntersection(rightBottomPoint, rightTopPoint, rayStart, rayEnd, out tempIntersectionPoint))
        {
            Log.Message("Перечесение с правой гранью в точке " + tempIntersectionPoint);
            intersectionPoint = NearestPoint(intersectionPoint, tempIntersectionPoint, rayStart, ref minDistance);
            intersection = true;
        }

        if (LineExtensions.LinesIntersection(rightTopPoint, leftTopPoint, rayStart, rayEnd, out tempIntersectionPoint))
        {
            Log.Message("Перечесение с верхней гранью в точке " + tempIntersectionPoint);
            intersectionPoint = NearestPoint(intersectionPoint, tempIntersectionPoint, rayStart, ref minDistance);
            intersection = true;
        }

        if (!intersection) Log.Message("Пересечений с гранями не обнаружено.");
        return intersection;
    }

    static Vector2 NearestPoint(Vector2 lastPoint, Vector2 newPoint, Vector2 rayStart, ref float lastDistance)
    {
        var distance = Vector2.Distance(newPoint, rayStart);

        if (distance < lastDistance)
        {
            lastPoint = newPoint;
            lastDistance = distance;

            Log.Message("Установка ближайшей к началу луча точки пересечения: " + lastPoint);
        }

        return lastPoint;
    }
}
