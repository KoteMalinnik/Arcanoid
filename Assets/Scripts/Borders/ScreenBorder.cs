using System;
using UnityEngine;

public static class ScreenBorder
{
    #region Events

    public static event Action<MoveableBorder, BorderPosition> OnScreenBorderCollisionEnter = null;
    public static event Action<MoveableBorder, BorderPosition> OnScreenBorderCollisionExit = null;

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
                OnCollisionEnter(objBorder);
                return;
            }

            return;
        }

        if (!collisionExited)
        {
            OnCollisionExit(objBorder);
        }
    }

    static void OnCollisionEnter(MoveableBorder objBorder)
    {
        collisionEntered = true;
        collisionExited = false;
        OnScreenBorderCollisionEnter?.Invoke(objBorder, GetBorderPosition(objBorder));
    }

    static void OnCollisionExit(MoveableBorder objBorder)
    {
        collisionEntered = false;
        collisionExited = true;
        OnScreenBorderCollisionExit?.Invoke(objBorder, GetBorderPosition(objBorder));
    }

    static BorderPosition GetBorderPosition(MoveableBorder objBorder)
    {
        if (objBorder.TopLeftPoint.x <= border.TopLeftPoint.x) return BorderPosition.Left;
        if (objBorder.TopLeftPoint.y >= border.TopLeftPoint.y) return BorderPosition.Top;
        if (objBorder.BottomRightPoint.x >= border.BottomRightPoint.x) return BorderPosition.Right;
        return BorderPosition.Bottom;
    }
}
