using UnityEngine;
using System;

/// <summary>
/// Информация о столкновении.
/// </summary>
public class CollisionData
{
    /// <summary>
    /// Рамка объекта столкновения.
    /// </summary>
    public StaticBorder collidedBorder { get; }

    /// <summary>
    /// Позиция столкновения на рамке объекта столкновения.
    /// </summary>
    public Vector2 collisionPoint { get; }

    public CollisionData(StaticBorder collidedBorder, Vector2 collisionPoint)
    {
        if (collidedBorder == null) throw new ArgumentNullException();

        this.collidedBorder = collidedBorder;
        this.collisionPoint = collisionPoint;
    }
}
