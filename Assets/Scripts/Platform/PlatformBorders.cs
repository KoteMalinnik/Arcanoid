using UnityEngine;

public static class PlatformBorders
{
	#region Fields

	static Vector2 _point_TopLeft = Vector2.zero;
	static Vector2 _point_BottomRight = Vector2.zero;

	#endregion

	#region Properties

	public static Vector2 point_TopLeft => _point_TopLeft;
	public static Vector2 point_BottomRight => _point_BottomRight;

	#endregion

	public static void UpdateBorders(Transform platformTransform)
    {
		_point_TopLeft.y = platformTransform.position.y + platformTransform.localScale.y / 2;
		_point_TopLeft.x = platformTransform.position.x - platformTransform.localScale.x / 2;

		_point_BottomRight.y = platformTransform.position.y - platformTransform.localScale.y / 2;
		_point_BottomRight.x = platformTransform.position.x + platformTransform.localScale.x / 2;
	}
}
