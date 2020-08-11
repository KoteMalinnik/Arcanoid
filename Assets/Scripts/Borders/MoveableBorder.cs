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

    private void Update()
    {
		_border.Update(transform);
	}

    #endregion

    public void CheckCollisionWith(MoveableBorder objBorder)
    {
        if (Intersection(objBorder.border))
        {
            if (!collisionEntered)
            {
                CollisionEnter(objBorder);
                return;
            }

            return;
        }

        if (!collisionExited)
        {
            CollisionExit(objBorder);
        }
    }

    void CollisionEnter(MoveableBorder objBorder)
    {
        Log.Message($"{name}.OnCollisionEnter: {objBorder.name}.");
        collisionEntered = true;
        collisionExited = false;
        OnBorderCollisionEnter?.Invoke(objBorder);
    }

    void CollisionExit(MoveableBorder objBorder)
    {
        Log.Message($"{name}.OnCollisionExit: {objBorder.name}.");
        collisionEntered = false;
        collisionExited = true;
        OnBorderCollisionExit?.Invoke(objBorder);
    }

    bool Intersection(BorderData otherBorder)
    {
        return (
            border.TopLeftPoint.y < otherBorder.BottomRightPoint.y ||
            border.BottomRightPoint.y > otherBorder.TopLeftPoint.y ||
            border.BottomRightPoint.x < otherBorder.TopLeftPoint.x ||
            border.TopLeftPoint.x > otherBorder.BottomRightPoint.x);
    }
}
