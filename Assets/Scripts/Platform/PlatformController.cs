using UnityEngine;

[RequireComponent(typeof(PlatformMovement))]
[RequireComponent(typeof(MoveableBorder))]
public class PlatformController : MonoBehaviour
{
	#region Fields

	PlatformMovement movement = null;

	#endregion


    #region MonoBehaviour Callbacks

    private void Awake()
    {
		movement = GetComponentInChildren<PlatformMovement>();
	}

	private void OnEnable()
	{
		ScreenBorder.OnScreenBorderCollisionEnter += StopMovementAtDirection;
		ScreenBorder.OnScreenBorderCollisionExit += AllowMovementAtAllDirections;
	}

	private void OnDisable()
	{
		ScreenBorder.OnScreenBorderCollisionEnter -= StopMovementAtDirection;
		ScreenBorder.OnScreenBorderCollisionExit -= AllowMovementAtAllDirections;
	}

	#endregion

	void StopMovementAtDirection(MoveableBorder border)
    {
		if (border.gameObject != gameObject) return;

		if (border.transform.position.x > 0) //платформа уперлась в правый край экрана
        {
			Log.Message("Платформа уперлась вправо.");
			movement.AllowMovement(true, false);
			return;
        }
		else
        {
			Log.Message("Платформа уперлась влево.");
			movement.AllowMovement(false, true);
			return;
		}
	}

	void AllowMovementAtAllDirections(MoveableBorder border)
    {
		if (border.gameObject != gameObject) return;

		movement.AllowMovement(true, true);
	}
}
