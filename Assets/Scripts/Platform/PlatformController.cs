using UnityEngine;

[RequireComponent(typeof(PlatformMovement))]
public class PlatformController : MonoBehaviour
{
	#region Fields

	PlatformMovement movement = null;

	#endregion


    #region MonoBehaviour Callbacks

    private void Start()
    {
		movement = GetComponent<PlatformMovement>();
	}

	private void OnEnable()
	{

	}

	private void OnDisable()
	{

	}

    #endregion
}
