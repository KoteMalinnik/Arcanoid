using UnityEngine;
using System;

[RequireComponent(typeof(BallMovement))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallMovementController : MonoBehaviour
{
	#region Events

	public static event Action OnBallTouchesBottom = null;

	#endregion

	#region Fields

	BallMovement movement = null;

    #endregion


    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
		BonusController.OnBonusReceive += Accelerate;
		BonusController.OnBonusReceive += Slowdown;
	}

    private void OnDisable()
    {
		BonusController.OnBonusReceive -= Accelerate;
		BonusController.OnBonusReceive -= Slowdown;
	}

    private void Awake()
	{
		Component check = null;
		if (!TryGetComponent(typeof(BallLaunching), out check)) gameObject.AddComponent<BallLaunching>();

		movement = GetComponent<BallMovement>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.name.Equals("Bottom"))
        {
			Log.Warning("Касание Bottom.");
			OnBallTouchesBottom?.Invoke();
			Destroy(gameObject);
			return;
        }

		if (collision.contactCount == 0)
        {
			Log.Warning("Контактов не обнаружено");
			return;
        }

		if (collision.gameObject.tag.Equals(tag))
        {
			Log.Message("Столкновение с шаром. Игнорирование.");
			return;
        }

		var contact = collision.contacts[0];
		var reflectedDirection = Vector2.Reflect(movement.Direction, contact.normal);

#if UNITY_EDITOR
		Debug.DrawRay(contact.point, contact.normal, Color.red, 1);
		Debug.DrawRay(contact.point, -movement.Direction, Color.blue, 1);
		Debug.DrawRay(contact.point, reflectedDirection, Color.green, 1);
#endif

		movement.SetDirection(reflectedDirection);
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
