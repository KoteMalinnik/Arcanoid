using UnityEngine;

public class BallMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] bool canMove = false;
    [Space(10)]
    [SerializeField] float movementSpeed = 1.0f;

    Vector3 direction = Vector2.up;
    Transform cachedTransform = null;

    #endregion

    #region Properties

    public new Transform transform
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
    }

    private void Update()
    {
        if (!canMove) return;

        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    #endregion

    /// <summary>
    /// Установит разрешения движения шара. Если true, то шар будет двигаться.
    /// </summary>
    /// <param name="premission">Разрешение движения.</param>
    public void SetMovementPremission(bool premission)
    {
        canMove = premission;
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
