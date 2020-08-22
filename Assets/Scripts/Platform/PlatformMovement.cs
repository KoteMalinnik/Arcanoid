using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] float movementSpeed = 1.0f;

	[Header("Управление")]

	[SerializeField] KeyCode keyToLeft = KeyCode.A;
	[SerializeField] KeyCode keyToRight = KeyCode.D;

	new Transform transform = null;

	float movementThreshold = 1;

	#endregion

	#region Properties

	bool allowMovementToTheLeft => transform.position.x > -movementThreshold;
	bool allowMovementToTheRight => transform.position.x < movementThreshold;

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
		transform = base.transform;
		RecalculateThreshold(BonusType.PlatformWidthIncreasing);
	}

    private void Update()
    {
		if (allowMovementToTheLeft && Input.GetKey(keyToLeft)) Move(-1);
		if (allowMovementToTheRight && Input.GetKey(keyToRight)) Move(1);
	}

    private void OnEnable()
    {
		BonusController.OnBonusReceive += RecalculateThreshold;    
    }

    private void OnDisable()
    {
		BonusController.OnBonusReceive -= RecalculateThreshold;
	}

    #endregion

    private void Move(int direction)
    {
		float deltaPosition = direction * movementSpeed * Time.deltaTime;
		var newPosition = new Vector2(transform.position.x + deltaPosition, transform.position.y);
		transform.position = newPosition;
	}

	void RecalculateThreshold(BonusType bonusType)
    {
		if (bonusType != BonusType.PlatformWidthIncreasing) return;

		float rightScreenBorder = -Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).x;
		float sizeX = GetComponent<SpriteRenderer>().size.x;

		movementThreshold = rightScreenBorder - sizeX / 2;
	}
}
