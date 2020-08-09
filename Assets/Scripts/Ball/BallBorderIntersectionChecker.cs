using System;
using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class BallBorderIntersectionChecker : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Пересечение шаром границы экрана. Аргументы: контроллер движения шара, граница.
    /// </summary>
    public static event Action<BallMovement> OnBorderIntersection = null;

    #endregion

    #region Fields

    BallMovement ballMovement= null;

    float threshold_Left = 0;
    float threshold_Right = 0;
    float threshold_Top = 0;
    float threshold_Bottom = 0;

    #endregion

    #region Properties

    bool LeftBorderIntersection => ballMovement.transform.position.x < threshold_Left;
    bool RightBorderIntersection => ballMovement.transform.position.x > threshold_Right;
    bool TopBorderIntersection => ballMovement.transform.position.y > threshold_Top;
    bool BottomBorderIntersection => ballMovement.transform.position.y < threshold_Bottom;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        ballMovement = GetComponent<BallMovement>();

        LevelBorders.Initialize();

        //не недо проводить одни и те же рассчеты в *BorderIntersection-свойствах
        Vector2 ballHalfSize = ballMovement.transform.localScale / 2;
        threshold_Left = LevelBorders.border_Left + ballHalfSize.x;
        threshold_Right = LevelBorders.border_Right - ballHalfSize.x;
        threshold_Top = LevelBorders.border_Top - ballHalfSize.y;
        threshold_Bottom = LevelBorders.border_Bottom + ballHalfSize.y;
    }

    private void Update()
    {
        if (LeftBorderIntersection || RightBorderIntersection || TopBorderIntersection || BottomBorderIntersection)
        {
            Log.Message($"Пересечение границы в позиции: {ballMovement.transform.position}.");
            OnBorderIntersection?.Invoke(ballMovement);
        }
    }

    #endregion
}
