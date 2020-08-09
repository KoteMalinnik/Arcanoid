using UnityEngine;

public static class PlatformBorders
{
	#region Fields

	static RectBorder border = null;

	#endregion

	#region Properties

	public static Vector2 TopLeftPoint => border.TopLeftPoint;

	#endregion

	public static void UpdateBorder(Transform platformTransform)
    {
		border.UpdateBorder(platformTransform);
	}
}
