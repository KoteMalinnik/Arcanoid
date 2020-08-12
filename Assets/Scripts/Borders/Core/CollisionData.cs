using UnityEngine;

public class CollisionData
{
    public Vector2 IntersectionPoint { get; }
    public StaticCollider CollisedBorder { get; }

    public CollisionData(StaticCollider CollisedBorder, Vector2 IntersectionPoint)
    {
        this.CollisedBorder = CollisedBorder;
        this.IntersectionPoint = IntersectionPoint;
    }
}
