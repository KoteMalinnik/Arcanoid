using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	enum Direction
    {
		Left,
		Right
    }

	#region Fields

	[SerializeField] float movementSpeed = 1.0f;

	[Header("Управление")]

	[SerializeField] bool inverce = false;
	[Space(10)]
	[SerializeField] KeyCode keyToLeft = KeyCode.A;
	[SerializeField] KeyCode keyToRight = KeyCode.D;

	new Transform transform = null;

	bool allowMovementToTheLeft = true;
	bool allowMovementToTheRight = true;

	#endregion

	#region MonoBehaviour Callbacks

	private void OnValidate()
    {
		movementSpeed = Extencions.MinThreshold(movementSpeed, 0);

		if (keyToLeft == KeyCode.None) Log.Warning("Клавиша перемещения платформы влево не назначена.");
		if (keyToRight == KeyCode.None) Log.Warning("Клавиша перемещения платформы вправо не назначена.");
	}

    private void Awake()
    {
		transform = transform.parent;
	}

    private void Update()
    {
		if (allowMovementToTheLeft && Input.GetKey(keyToLeft)) Move(Direction.Left);
		if (allowMovementToTheRight && Input.GetKey(keyToRight)) Move(Direction.Right);
	}

	#endregion

	private void Move(Direction direction)
    {
		float deltaPosition = movementSpeed * Time.deltaTime;
		deltaPosition = direction == Direction.Left ? -deltaPosition : deltaPosition;
		if (inverce) deltaPosition = -deltaPosition;

		var newPosition = new Vector2(transform.position.x + deltaPosition, transform.position.y);
		transform.position = newPosition;
	}

	public void AllowMovement(bool toTheLeft, bool toTheRight)
    {
		allowMovementToTheLeft = toTheLeft;
		allowMovementToTheRight = toTheRight;
    }
}
