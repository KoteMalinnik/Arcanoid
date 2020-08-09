using UnityEngine;

public class BallMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] bool allowMovement = false;
    [Space(10)]
    [SerializeField] float movementSpeed = 1.0f;

    Vector3 direction = Vector2.down;

    new Transform transform = null;

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

    /// <summary>
    /// Установит разрешения движения шара. Если true, то шар будет двигаться.
    /// </summary>
    /// <param name="premission">Разрешение движения.</param>
    public void AllowMovement(bool premission)
    {
        allowMovement = premission;
    }

    /// <summary>
    /// Изменит направление движения шара.
    /// </summary>
    /// <param name="enumDirection">Направление.</param>
    public void SetDirection(BallMovementDirections enumDirection)
    {
        Log.Message("Смена направления движения шара.");

        switch(enumDirection)
        {
            case BallMovementDirections.ToLeftBottom:
                direction = new Vector2(-1, -1);
                break;
            case BallMovementDirections.ToLeftTop:
                direction = new Vector2(-1, 1);
                break;
            case BallMovementDirections.ToRightBottom:
                direction = new Vector2(1, -1);
                break;
            case BallMovementDirections.ToRightTop:
                direction = new Vector2(1, 1);
                break;
        }
    }
}
