using UnityEngine;
using System;

[RequireComponent(typeof(BallMovement))]
[RequireComponent(typeof(MoveableCollider))]
public class BallController : MonoBehaviour
{
	#region Events

	public static event Action OnBallTouchesBottom = null;

	#endregion

	#region Fields

	BallMovement movement = null;
	new MoveableCollider collider = null;

	#endregion


	#region MonoBehaviour Callbacks

	private void Awake()
	{
		Component check = null;
		//if (!TryGetComponent(typeof(BallLaunching), out check)) gameObject.AddComponent<BallLaunching>();

		movement = GetComponent<BallMovement>();
		collider = GetComponent<MoveableCollider>();
	}

	private void OnEnable()
	{
		MoveableCollider.OnBorderCollisionEnter += ChangeDirection;
	}

	private void OnDisable()
	{
		MoveableCollider.OnBorderCollisionEnter -= ChangeDirection;
	}

	CollisionData nextCollisionData = null;

	private void Update()
    {
        if (BordersSearch.CollisionAtDirection(collider, transform.position, movement.Direction, out nextCollisionData))
        {
            if (nextCollisionData == null) return;
            collider.CheckCollisionWith(nextCollisionData);
        }
    }

    #endregion

    void ChangeDirection(CollisionData collision)
    {
		//В каждом направлении возможно два варианта следующего направления
		//Зависит от положения шара относительно объекта

		//если направление ToLeftBottom : либо ToLeftTop, либо ToRightBottom
		// ToLeftTop, если:
		// transform.position.x > staticBorder.transform.position.x


		if (collision.IntersectionPoint.x.Equals(collision.CollisedBorder.Border.Right))
		{
			Log.Warning("Касание правой стороны рамки.");
		}

		if (collision.IntersectionPoint.x.Equals(collision.CollisedBorder.Border.Left))
		{
			Log.Warning("Касание левой стороны рамки.");
		}

		if (collision.IntersectionPoint.y.Equals(collision.CollisedBorder.Border.Top))
		{
			Log.Warning("Касание верхней стороны рамки.");
		}

		if (collision.IntersectionPoint.y.Equals(collision.CollisedBorder.Border.Bottom))
		{
			Log.Warning("Касание нижней стороны рамки.");
		}

		//if (movement.Direction.Equals(Directions.ToLeftBottom))
		//{



		//}
	}
}
