using System;
using UnityEngine;

public class BallPlatformIntersectionChecker : MonoBehaviour
{
    //#region Events

    ///// <summary>
    ///// Касание шаром платформы. Аргументы: контроллер движения шара, граница.
    ///// </summary>
    //public static event Action<BallMovement> OnPlatformTouch = null;

    //#endregion

    //#region Fields

    //bool abovePlatformFirst = false;

    //float threshold_Left = 0;
    //float threshold_Right = 0;
    //float threshold_Top = 0;
    //float threshold_Bottom = 0;

    //Transform cachedTransform = null;

    //#endregion

    //#region Properties

    //new Transform transform
    //{
    //    get
    //    {
    //        if (cachedTransform == null) cachedTransform = base.transform.parent;
    //        return cachedTransform;
    //    }
    //}

    //bool Intersection_LeftBorder => transform.position.x > threshold_Left;
    //bool Intersection_RightBorder => transform.position.x < threshold_Right;
    //bool Intersection_TopBorder => transform.position.y < threshold_Top;
    //bool Intersection_BottomBorder => transform.position.y > threshold_Bottom;

    ///// <summary>
    ///// Шар находится между правой и левой границами платформы
    ///// </summary>
    //bool AbovePlatform => Intersection_LeftBorder && Intersection_RightBorder;
    ///// <summary>
    ///// Шар находится между верхней и нижней границами платформы.
    ///// </summary>
    //bool OnTheSidesOfPlatform => Intersection_TopBorder && Intersection_BottomBorder;

    //#endregion

    //#region MonoBehaviour Callbacks

    //private void Start()
    //{
    //    //не недо проводить одни и те же рассчеты в *BorderIntersection-свойствах
    //    Vector2 ballHalfSize = transform.localScale / 2;

    //    threshold_Left = PlatformBorders.point_TopLeft.x - ballHalfSize.x;
    //    threshold_Top = PlatformBorders.point_TopLeft.y + ballHalfSize.y;

    //    threshold_Right = PlatformBorders.point_BottomRight.x + ballHalfSize.x;
    //    threshold_Bottom = PlatformBorders.point_BottomRight.y - ballHalfSize.y;
    //}

    //private void Update()
    //{
    //    if (AbovePlatform && !OnTheSidesOfPlatform) abovePlatformFirst = true;
    //    if (!AbovePlatform && OnTheSidesOfPlatform) abovePlatformFirst = false;

    //    if (!abovePlatformFirst) return;

    //    if (AbovePlatform && OnTheSidesOfPlatform) //Касание платформы сверху
    //    {
    //        //узнать, с какой стороны коснулся (сверху или сбоку)
    //        //Обрабатывать как касание сверху можно только тогда, когда сначала AbovePlatform, а потом уже OnTheSidesOfPlatform
    //        Log.Message($"Касание платформы сверху.");
    //        OnPlatformTouch?.Invoke(transform.GetComponentInChildren<BallMovement>());
    //        abovePlatformFirst = false;
    //        return;
    //    }
    //}

    //#endregion
}
