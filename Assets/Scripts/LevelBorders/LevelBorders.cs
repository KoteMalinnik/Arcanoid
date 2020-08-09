using UnityEngine;

public static class LevelBorders
{
	#region Fields

    static float leftBorder = 0;
    static float rightBorder = 0;
    static float topBorder = 0;
    static float bottomBorder = 0;

    #endregion

    #region Properties

    public static float border_Left => leftBorder;
    public static float border_Right => rightBorder;
    public static float border_Top => topBorder;
    public static float border_Bottom => bottomBorder;

    #endregion

    public static void Initialize()
    {
        Log.Message("Установка границ.");

        GetBorders(out leftBorder, out topBorder, screenHeight: Screen.height);
        GetBorders(out rightBorder, out bottomBorder, screenWidth: Screen.width);
        
        Log.Message(
            $"Левый край: {leftBorder}." +
            $"Правый край: {rightBorder}." +
            $"Верхний край: {topBorder}." +
            $"Нижний край: {bottomBorder}."
            );
    }

    static void GetBorders(out float borderX, out float borderY, float screenWidth = 0, float screenHeight = 0)
    {
        Vector2 screenCornerPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));

        borderX = screenCornerPoint.x;
        borderY = screenCornerPoint.y;
    }
}
