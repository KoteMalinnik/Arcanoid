using UnityEngine;

public class BallMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] bool allowMovement = false;
    [SerializeField] float movementSpeed = 1.0f;

    Vector3 direction = Directions.ToLeftTop;

    new Transform transform = null;

    #endregion

    #region Properties

    public Vector2 Direction => direction;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        movementSpeed = Extencions.MinThreshold(movementSpeed, 0);
    }

    private void Awake()
    {
        transform = base.transform;
    }

    private void Update()
    {
        if (allowMovement) Move();
    }

    #endregion

    void Move()
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    #region Public Methods

    public void AllowMovement()
    {
        allowMovement = true;
    }

    public void DisallowMovement()
    {
        allowMovement = false;
    }

    /// <summary>
    /// Изменит направление движения шара.
    /// </summary>
    /// <param name="enumDirection">Направление.</param>
    public void SetDirection(Vector2 newDirection)
    {
        Log.Message($"Смена направления движения шара: {direction} -> {newDirection}.");
        direction = newDirection;
    }

    #endregion
}
