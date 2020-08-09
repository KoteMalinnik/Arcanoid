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

	Transform cachedTransform = null;

	float threshold_Left = 0;
	float threshold_Right = 0;

	#endregion

	#region Properties

	new Transform transform
    {
		get
        {
			if (cachedTransform == null) cachedTransform = base.transform.parent;
			return cachedTransform;
        }
    }

	bool LeftBorderIntersection => transform.position.x < threshold_Left;
	bool RightBorderIntersection => transform.position.x > threshold_Right;

	#endregion

	#region MonoBehaviour Callbacks

	private void OnValidate()
    {
		movementSpeed = Extencions.MinThreshold(movementSpeed, 0);

		if (keyToLeft == KeyCode.None) Log.Warning("Клавиша перемещения платформы влево не назначена.");
		if (keyToRight == KeyCode.None) Log.Warning("Клавиша перемещения платформы вправо не назначена.");
	}

    private void Start()
    {
		ScreenBorders.Initialize();

		//не недо проводить одни и те же рассчеты в *BorderIntersection-свойствах
		float platformHalfSizeX = transform.localScale.x / 2;

		threshold_Left = ScreenBorders.point_TopLeft.x + platformHalfSizeX;
		threshold_Right = ScreenBorders.point_BottomRight.x - platformHalfSizeX;
		
		PlatformBorders.UpdateBorders(transform);
	}

    private void Update()
    {
		if (!LeftBorderIntersection && Input.GetKey(keyToLeft)) Move(Direction.Left);
		if (!RightBorderIntersection && Input.GetKey(keyToRight)) Move(Direction.Right);
	}

    #endregion

	void Move(Direction direction)
    {
		float deltaPosition = movementSpeed * Time.deltaTime;
		deltaPosition = direction == Direction.Left ? -deltaPosition : deltaPosition;
		if (inverce) deltaPosition = -deltaPosition;

		var newPosition = new Vector2(transform.position.x + deltaPosition, transform.position.y);
		transform.position = newPosition;

		PlatformBorders.UpdateBorders(transform);
	}
}
