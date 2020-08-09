using System;
using UnityEngine;

public static class ScreenBorder
{
    #region Events

    public static event Action<MoveableBorder> OnScreenBorderCollisionEnter = null;
    public static event Action<MoveableBorder> OnScreenBorderCollision = null;
    public static event Action<MoveableBorder> OnScreenBorderCollisionExit = null;

    #endregion

    #region Fields

    static bool collisionEntered = false;
    static bool collisionExited = true;

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
            //Log.Message($"Касание границ экрана объектом {objBorder.name}.");

            if (!collisionEntered)
            {
                collisionEntered = true;
                collisionExited = false;
                OnScreenBorderCollisionEnter?.Invoke(objBorder);
                return;
            }

            OnScreenBorderCollision?.Invoke(objBorder);
            return;
        }

        if (!collisionExited)
        {
            collisionEntered = false;
            collisionExited = true;
            OnScreenBorderCollisionExit?.Invoke(objBorder);
        }
    }
}
