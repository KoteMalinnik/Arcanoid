using UnityEngine;
using System;

[RequireComponent(typeof(BallLaunching))]
[RequireComponent(typeof(BallMovement))]
[RequireComponent(typeof(MoveableBorder))]
public class BallController : MonoBehaviour
{
	#region Events

	public static event Action OnBallTouchesBottom = null;

	#endregion

	#region Fields

	BallMovement movement = null;

	#endregion


	#region MonoBehaviour Callbacks

	private void Awake()
	{
		movement = GetComponent<BallMovement>();
	}

	private void OnEnable()
	{
		ScreenBorder.OnScreenBorderCollisionEnter += ScreenBorderCollissionHandler;
	}

	private void OnDisable()
	{
		ScreenBorder.OnScreenBorderCollisionEnter -= ScreenBorderCollissionHandler;
	}

	#endregion

	void ScreenBorderCollissionHandler(MoveableBorder border, BorderPosition borderPosition)
	{
		if (border.gameObject != gameObject) return;

		if (borderPosition == BorderPosition.Bottom)
        {
			Log.Message("Конец игры.");
			gameObject.SetActive(false);
			OnBallTouchesBottom?.Invoke();
			return;
        }

		BallMovementDirections previousDirection = movement.EnumDirection;

		switch (borderPosition)
        {
			case BorderPosition.Left:
				movement.SetDirection(previousDirection == BallMovementDirections.ToLeftBottom ?
					BallMovementDirections.ToRightBottom : BallMovementDirections.ToRightTop);
				break;
			case BorderPosition.Right:
				movement.SetDirection(previousDirection == BallMovementDirections.ToRightBottom ?
					BallMovementDirections.ToLeftBottom : BallMovementDirections.ToLeftTop);
				break;
			case BorderPosition.Top:
				movement.SetDirection(previousDirection == BallMovementDirections.ToRightTop ?
					BallMovementDirections.ToRightBottom : BallMovementDirections.ToLeftBottom);
				break;
		}
	}
}
