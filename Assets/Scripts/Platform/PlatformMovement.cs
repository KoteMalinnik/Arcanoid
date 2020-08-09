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

	[Header("Ограничения")]

	[SerializeField] float maxDistanceFormCenter = 5.0f;

	Transform cachedTransform = null;

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

    private void OnValidate()
    {
		movementSpeed = Extencions.MinThreshold(movementSpeed, 0);

		if (keyToLeft == KeyCode.None) Log.Warning("Клавиша перемещения платформы влево не назначена.");
		if (keyToRight == KeyCode.None) Log.Warning("Клавиша перемещения платформы вправо не назначена.");

		maxDistanceFormCenter = Extencions.MinThreshold(maxDistanceFormCenter, transform.localScale.x / 2);
	}

    private void Awake()
    {
		maxDistanceFormCenter -= transform.localScale.x / 2; //учет размера платформы
		
		PlatformBorders.SetTopBorder(transform);
		PlatformBorders.UpdateSideBorders(transform);
	}

    private void Update()
    {
		if (Input.GetKey(keyToLeft)) Move(Direction.Left);
		if (Input.GetKey(keyToRight)) Move(Direction.Right);
	}

    #endregion

	void Move(Direction direction)
    {
		float deltaPosition = movementSpeed * Time.deltaTime;
		deltaPosition = direction == Direction.Left ? -deltaPosition : deltaPosition;

		if (inverce) deltaPosition = -deltaPosition;

		var newPosition = new Vector2(transform.position.x + deltaPosition, transform.position.y);
		
		if (Mathf.Abs(newPosition.x) > maxDistanceFormCenter) return;

		transform.position = newPosition;
		PlatformBorders.UpdateSideBorders(transform);
	}
}
