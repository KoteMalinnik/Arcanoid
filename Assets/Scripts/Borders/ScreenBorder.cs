using System;
using UnityEngine;

public static class ScreenBorder
{
    #region Events

    public static event Action<MoveableBorder> OnScreenBorderCollision = null;

    #endregion

    #region Fields

    static Border border = null;
    
    #endregion

    static void Initialize()
    {
        if (border != null) return;

        Log.Message("Установка границ экрана.");

        Vector2 topLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 bottomRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        border = new Border();
        border.UpdateBorder(topLeftPoint, bottomRightPoint);

        Log.Message($"Левый верхний край: {border.TopLeftPoint}. Правый нижний край: {border.BottomRightPoint}.");
    }

    public static void CheckIntersection(MoveableBorder objBorder)
    {
        if (border == null) Initialize();

        if (objBorder.TopLeftPoint.x <= border.TopLeftPoint.x ||
            objBorder.TopLeftPoint.y >= border.TopLeftPoint.y ||
            objBorder.BottomRightPoint.x >= border.BottomRightPoint.x ||
            objBorder.BottomRightPoint.y <= border.BottomRightPoint.y)
        {
            OnScreenBorderCollision?.Invoke(objBorder);
        }
    }
}
