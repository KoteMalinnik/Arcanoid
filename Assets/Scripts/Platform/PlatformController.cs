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
		ScreenBorder.OnScreenBorderCollision += StopMovementAtDirection;
	}

	private void OnDisable()
	{
		ScreenBorder.OnScreenBorderCollision -= StopMovementAtDirection;
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
}
