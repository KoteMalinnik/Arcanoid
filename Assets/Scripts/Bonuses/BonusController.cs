using System;
using UnityEngine;

[RequireComponent(typeof(BonusMovement))]
public class BonusController : MonoBehaviour
{
	#region Events

	public static event Action<BonusType> OnBonusReceive = null;

	#endregion

	#region Fields

	BonusType bonusType;

	BoxCollider2D cachedCollider = null;
	BoxCollider2D platformCollider = null;

	#endregion

    #region MonoBehaviour Callbacks

    private void LateUpdate()
    {
		Vector2 closestPlatformColliderPoint = platformCollider.ClosestPoint(transform.position);

		if (cachedCollider.OverlapPoint(closestPlatformColliderPoint))
		{
			Log.Message($"Касание бонусом {name} платформы.");

			OnBonusReceive?.Invoke(bonusType);
			Destroy(gameObject);
		}
	}

    #endregion

    public void Initialize(BonusType bonusType, float movementSpeed)
    {
		Log.Message("Инициализация объекта бонуса.");

		tag = "Bonus";

		this.bonusType = bonusType;
		GetComponent<BonusMovement>().Initialize(movementSpeed);

		cachedCollider = GetComponent<BoxCollider2D>();
		platformCollider = GameObject.FindObjectOfType<PlatformMovement>(true).GetComponent<BoxCollider2D>();

		Log.Message("Инициализация объекта бонуса завершена.");
	}
}
