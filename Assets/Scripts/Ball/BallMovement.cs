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
    /// <param name="newDirection">Направление.</param>
    public void ChangeDirection(Vector2 newDirection)
    {
        Log.Message("Смена направления движения шара.");

        newDirection.Normalize();
        if (newDirection.magnitude < 1)
        {
            Log.Warning($"Передано нулевое направление: {newDirection.normalized}.");
            Log.Message("Отмена смены направления.");
            return;
        }

        direction = newDirection;
    }
}
