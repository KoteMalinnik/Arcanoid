using UnityEngine;
using System;

[RequireComponent(typeof(BallMovement))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallCollisionController : MonoBehaviour
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
		reflectedDirection = Directions.GetCorrectDirection(reflectedDirection);

#if UNITY_EDITOR
		Debug.DrawRay(contact.point, contact.normal, Color.red, 1);
		Debug.DrawRay(contact.point, -movement.Direction, Color.blue, 1);
		Debug.DrawRay(contact.point, reflectedDirection, Color.green, 1);
#endif

		movement.SetDirection(reflectedDirection);
	}

	#endregion
}
