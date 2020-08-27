using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class BallBonusesHandler : MonoBehaviour
{
	#region Fields

	BallMovement movement = null;

    #endregion


    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
		BonusPlatformTouchController.OnBonusReceive += Accelerate;
		BonusPlatformTouchController.OnBonusReceive += Slowdown;
	}

    private void OnDisable()
    {
		BonusPlatformTouchController.OnBonusReceive -= Accelerate;
		BonusPlatformTouchController.OnBonusReceive -= Slowdown;
	}

    private void Awake()
	{
		Component check = null;
		if (!TryGetComponent(typeof(BallLaunching), out check)) gameObject.AddComponent<BallLaunching>();

		movement = GetComponent<BallMovement>();
	}

    #endregion

	void Accelerate(BonusType bonusType)
    {
		if (bonusType != BonusType.BallAcceleration) return;
		
		Log.Message("Ускорение.");
		movement.ChangeSpeed(2);
	}

	void Slowdown(BonusType bonusType)
    {
		if (bonusType != BonusType.BallSlowdown) return;

		Log.Message("Замедление.");
		movement.ChangeSpeed(0.5f);
	}
}
