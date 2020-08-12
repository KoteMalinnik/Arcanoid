using UnityEngine;

[RequireComponent(typeof(PlatformMovement))]
[RequireComponent(typeof(MoveableCollider))]
public class PlatformController : MonoBehaviour
{
	#region Fields

	PlatformMovement movement = null;

	#endregion


    #region MonoBehaviour Callbacks

    private void Start()
    {
		movement = GetComponent<PlatformMovement>();

		//StaticBorder leftScreenBorder = 
	}

	private void OnEnable()
	{
		//ScreenBorder.OnScreenBorderCollisionEnter += StopMovementAtDirection;
		//ScreenBorder.OnScreenBorderCollisionExit += AllowMovementAtAllDirections;
	}

	private void OnDisable()
	{
		//ScreenBorder.OnScreenBorderCollisionEnter -= StopMovementAtDirection;
		//ScreenBorder.OnScreenBorderCollisionExit -= AllowMovementAtAllDirections;
	}

    #endregion
}
