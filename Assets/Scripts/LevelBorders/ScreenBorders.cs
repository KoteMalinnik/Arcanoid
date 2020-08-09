using UnityEngine;

public static class ScreenBorders
{
    #region Fields

    static RectBorder border = null;

    #endregion

    #region Properties

    public static Vector2 TopLeftPoint => border.TopLeftPoint;

    #endregion

    public static void Initialize()
    {
        Log.Message("Установка границ экрана.");
        
        if (border != null)
        {
            Log.Message("Границы уже рассчитаны.");
            return;
        }

        border.UpdateBorder(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));

        Log.Message
            (
            $"Левый верхний край: {border.TopLeftPoint}." +
            $"Правый нижний край: {-border.TopLeftPoint}."
            );
    }
}
