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

	private void Awake()
	{
		Component check = null;
		if (!TryGetComponent(typeof(BallLaunching), out check)) gameObject.AddComponent<BallLaunching>();

		movement = GetComponent<BallMovement>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.name == "Bottom")
        {
			Log.Warning("КОНЕЦ ИГРЫ.");
			movement.DisallowMovement();
			OnBallTouchesBottom?.Invoke();
			return;
        }

		if (collision.contactCount == 0)
        {
			Log.Warning("Контактов не обнаружено");
			return;
        }

		var contact = collision.contacts[0];
		var reflectedDirection = Vector2.Reflect(movement.Direction, contact.normal);

#if UNITY_EDITOR
		Debug.DrawRay(contact.point, contact.normal, Color.red, 5);
		Debug.DrawRay(contact.point, -movement.Direction, Color.blue, 5);
		Debug.DrawRay(contact.point, reflectedDirection, Color.green, 5);
#endif

		movement.SetDirection(reflectedDirection);
    }

    #endregion
}
