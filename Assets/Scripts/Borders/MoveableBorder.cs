using UnityEngine;
using System;

public class MoveableBorder: StaticBorder
{
	#region Events

	public static event Action<StaticBorder> OnBorderCollisionEnter = null;
	public static event Action<StaticBorder> OnBorderCollisionExit = null;

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
		border.Update(transform);
        base.Update();
    }

    #endregion

    public void CheckCollisionWith(StaticBorder objBorder)
    {
        if (Intersection(objBorder.border))
        {
            if (!collisionExited)
            {
                CollisionExit(objBorder);
                return;
            }

            return;
        }

        if (!collisionEntered)
        {
            CollisionEnter(objBorder);
        }
    }

    void CollisionEnter(StaticBorder objBorder)
    {
        Log.Message($"{name}.OnCollisionEnter: {objBorder.name}.");
        collisionEntered = true;
        collisionExited = false;
        OnBorderCollisionEnter?.Invoke(objBorder);
    }

    void CollisionExit(StaticBorder objBorder)
    {
        Log.Message($"{name}.OnCollisionExit: {objBorder.name}.");
        collisionEntered = false;
        collisionExited = true;
        OnBorderCollisionExit?.Invoke(objBorder);
    }

    bool Intersection(BorderData otherBorder)
    {
        return
            border.Top < otherBorder.Bottom ||
            border.Bottom > otherBorder.Top ||
            border.Right < otherBorder.Left ||
            border.Left > otherBorder.Right;
    }
}
