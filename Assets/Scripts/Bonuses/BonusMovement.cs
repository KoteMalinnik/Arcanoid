using UnityEngine;

public class BonusMovement: MonoBehaviour
{
	#region Fields

	Transform cachedTransform = null;
	Vector2 newPosition = Vector2.zero;
	float movementSpeed = 1;

	float bottomBorder = -7;

	#endregion

	#region Properties

	new Transform transform
	{
		get
		{
			if (cachedTransform == null) cachedTransform = base.transform;
			return cachedTransform;
		}
	}

	#endregion

	#region MonoBehaviour Callbacks

	private void Update()
	{
		newPosition.y -= movementSpeed * Time.deltaTime;
		transform.position = newPosition;

		if (transform.position.y < bottomBorder)
        {
			Log.Message("Касание платформы не было. Бонус вне экрана. Уничтожение.");
			Destroy(gameObject);
		}
	}

	#endregion

	public void Initialize(float movementSpeed)
	{
		Log.Message("Инициализация движения объекта бонуса.");

		this.movementSpeed = movementSpeed <= 0 ? 1 : movementSpeed;
		newPosition = transform.position;

		Log.Message("Скорость: " + this.movementSpeed);
	}
}
