using UnityEngine;
using System.Collections.Generic;
using System;

public static class BordersSearch
{
    static List<StaticBorder> borders = null; 

    static void Initialize()
    {
        borders = new List<StaticBorder>();
        borders.AddRange(MonoBehaviour.FindObjectsOfType<StaticBorder>());
    }

    public static CollisionData FindAtDirection(Vector2 fromPoint, Vector2 direction, float duration = 5) //аналог Raycast(Ray ray, out Hit hit)
    {
        if (borders == null) Initialize();

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

        //foreach(StaticBorder rect in borders)
        //{
        // rect.border
        //}

        return null;
    }

    static bool Intersection(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, out Vector2 p)
    {
        var v1 = (endPos2.x - startPos2.x) * (startPos1.y - startPos2.y) - (endPos2.y - startPos2.y) * (startPos1.x - startPos2.x);
        var v2 = (endPos2.x - startPos2.x) * (endPos1.y - startPos2.y) - (endPos2.y - startPos2.y) * (endPos1.x - startPos2.x);
        var v3 = (endPos1.x - startPos1.x) * (startPos2.y - startPos1.y) - (endPos1.y - startPos1.y) * (startPos2.x - startPos1.x);
        var v4 = (endPos1.x - startPos1.x) * (endPos2.y - startPos1.y) - (endPos1.y - startPos1.y) * (endPos2.x - startPos1.x);

        p = Vector2.zero;

        return v1 * v2 < 0 && v3 * v4 < 0;
    }
}
