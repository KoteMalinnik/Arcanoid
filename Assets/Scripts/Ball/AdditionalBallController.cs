using UnityEngine;

public class AdditionalBallController : MonoBehaviour
{
    #region Fields

    static int additionalBallsCount = 0;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        BonusPlatformTouchController.OnBonusReceive += CreateBall;
        BallMovementController.OnBallTouchesBottom += DecrementAdditionalBallsCount;
    }

    private void OnDisable()
    {
        BonusPlatformTouchController.OnBonusReceive -= CreateBall;
        BallMovementController.OnBallTouchesBottom -= DecrementAdditionalBallsCount;
    }

    #endregion

    void CreateBall(BonusType bonusType)
    {
        if (bonusType != BonusType.AdditionalBall) return;

        Log.Message("Создание дополнительного шара.");

        var ballObject = GameObject.FindWithTag("Ball");
        Vector2 previousBallDirection = ballObject.GetComponent<BallMovement>().Direction;

        ballObject = Instantiate(ballObject);

        var ballLaunching = ballObject.GetComponent<BallLaunching>();
        Destroy(ballLaunching);

        var ballMovement = ballObject.GetComponent<BallMovement>();
        ballMovement.AllowMovement();
        ballMovement.SetDirection(Vector2.Perpendicular(previousBallDirection));

        additionalBallsCount++;
    }

    void DecrementAdditionalBallsCount()
    {
        additionalBallsCount--;

        if (additionalBallsCount < 0)
        {
            Log.Warning("Конец игры.");
            Debug.Break();
        }
    }
}
