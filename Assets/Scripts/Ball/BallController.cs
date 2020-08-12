using UnityEngine;
using System;

[RequireComponent(typeof(BallMovement))]
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
		Component check = null;
		//if (!TryGetComponent(typeof(BallLaunching), out check)) gameObject.AddComponent<BallLaunching>();

		movement = GetComponent<BallMovement>();
	}

	private void OnEnable()
	{

	}

	private void OnDisable()
	{

	}

	private void Update()
    {
        
    }

    #endregion
}
