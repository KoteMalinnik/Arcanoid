using System;
using UnityEngine;

public class BallBorderIntersectionChecker : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Пересечение шаром границы экрана. Аргументы: контроллер движения шара, граница.
    /// </summary>
    public static event Action<BallMovement> OnBorderIntersection = null;

    #endregion

    #region Fields

    float threshold_Left = 0;
    float threshold_Right = 0;
    float threshold_Top = 0;
    float threshold_Bottom = 0;

    Transform cachedTransform = null;

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
    bool TopBorderIntersection => transform.position.y > threshold_Top;
    bool BottomBorderIntersection => transform.position.y < threshold_Bottom;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        LevelBorders.Initialize();

        //не недо проводить одни и те же рассчеты в *BorderIntersection-свойствах
        Vector2 ballHalfSize = transform.localScale / 2;
        
        threshold_Left = LevelBorders.point_TopLeft.x + ballHalfSize.x;
        threshold_Top = LevelBorders.point_TopLeft.y - ballHalfSize.y;

        threshold_Right = LevelBorders.point_BottomRight.x - ballHalfSize.x;
        threshold_Bottom = LevelBorders.point_BottomRight.y + ballHalfSize.y;
    }

    private void Update()
    {
        if (LeftBorderIntersection || RightBorderIntersection || TopBorderIntersection || BottomBorderIntersection)
        {
            Log.Message($"Пересечение границы. Позиция шара: {transform.position}.");
            OnBorderIntersection?.Invoke(transform.GetComponentInChildren<BallMovement>());
        }
    }

    #endregion
}
