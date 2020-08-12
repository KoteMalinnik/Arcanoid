using UnityEngine;
using System;

public class MoveableCollider: StaticCollider
{
	#region Events

	public static event Action<CollisionData> OnBorderCollisionEnter = null;
	public static event Action<CollisionData> OnBorderCollisionExit = null;

	#endregion

	#region Fields

	bool collisionEntered = false;
	bool collisionExited = true;

	new Transform transform = null;

	#endregion

	#region MonoBehaviour Callbacks

	private new void Awake()
	{
		base.Awake();
		transform = base.transform;
	}

    private new void Update()
    {
		Border.Update(transform);
        base.Update();
    }

    #endregion

    public void CheckCollisionWith(CollisionData collision)
    {
        if (Intersection(collision.CollisedBorder.Border))
        {
            if (!collisionExited)
            {
                CollisionExit(collision);
                return;
            }

            return;
        }

        if (!collisionEntered)
        {
            CollisionEnter(collision);
        }
    }

    void CollisionEnter(CollisionData collision)
    {
        Log.Message($"{name}.OnCollisionEnter: {collision.CollisedBorder.name}.");
        collisionEntered = true;
        collisionExited = false;
        OnBorderCollisionEnter?.Invoke(collision);
    }

    void CollisionExit(CollisionData collision)
    {
        Log.Message($"{name}.OnCollisionExit: {collision.CollisedBorder.name}.");
        collisionEntered = false;
        collisionExited = true;
        OnBorderCollisionExit?.Invoke(collision);
    }

    bool Intersection(BorderData otherBorder)
    {
        return
            Border.Top < otherBorder.Bottom ||
            Border.Bottom > otherBorder.Top ||
            Border.Right < otherBorder.Left ||
            Border.Left > otherBorder.Right;
    }
}
