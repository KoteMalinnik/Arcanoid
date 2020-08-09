using UnityEngine;

public class BallLaunching : MonoBehaviour
{
    enum OnLaunchDirection
    {
        ToLeftTop,
        ToRightTop
    }

    #region Fields

    [SerializeField] OnLaunchDirection onLaunchDirection = OnLaunchDirection.ToLeftTop;
    [SerializeField] KeyCode launchKey = KeyCode.Space;

    bool isLaunched = false;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        if (launchKey == KeyCode.None) Log.Warning("Клавиша запуска шара не назначена.");
    }

    private void Start()
    {
        Log.Message("Ожидание нажатия кнопки запуска шара.");
    }

    private void Update()
    {
        if (isLaunched) return;

        if (Input.GetKeyDown(launchKey))
        {
            Log.Message("Кнопка запуска была нажата.");

            Launch();
            isLaunched = true;
        }
    }

    void Launch()
    {
        Log.Message("Запуск шара.");

        var ballMovement = transform.parent.GetComponentInChildren<BallMovement>();

        BallMovementDirections targetDirection = BallMovementDirections.ToLeftTop;

        switch (onLaunchDirection)
        {
            case OnLaunchDirection.ToLeftTop:
                targetDirection = BallMovementDirections.ToLeftTop;
                break;
            case OnLaunchDirection.ToRightTop:
                targetDirection = BallMovementDirections.ToRightTop;
                break;
        }

        ballMovement.SetDirection(targetDirection);
        ballMovement.SetMovementPremission(true);
    }
    #endregion
}
