using UnityEngine;

public static class LevelBorders
{
    #region Fields

    static Vector2 _point_TopLeft = Vector2.zero;
    static Vector2 _point_BottomRight = Vector2.zero;

    static bool initialized = false; //нет необходимости каждый новый уровень пересчитывать одно и то же.

    #endregion

    #region Properties

    public static Vector2 point_TopLeft => _point_TopLeft;
    public static Vector2 point_BottomRight => _point_BottomRight;

    #endregion

    public static void Initialize()
    {
        Log.Message("Установка границ.");
        
        if (initialized)
        {
            Log.Message("Границы уже рассчитаны.");
            return;
        }

        _point_TopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        _point_BottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        initialized = true;

        Log.Message
            (
            $"Левый верхний край: {_point_TopLeft}." +
            $"Правый нижний край: {_point_BottomRight}."
            );
    }
}
