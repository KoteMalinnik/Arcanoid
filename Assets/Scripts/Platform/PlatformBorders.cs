using UnityEngine;

public static class PlatformBorders
{
	#region Properties

	public static float border_Left { get; private set; } = 0;
	public static float border_Right { get; private set; } = 0;
	public static float border_Top { get; private set; } = 0;

	#endregion

	public static void SetTopBorder(Transform platformTransform)
    {
		//устанавливается один раз, т.к. платформа перемещается только горизонтально.
		border_Top = platformTransform.position.y + platformTransform.localScale.y / 2;
    }

	public static void UpdateSideBorders(Transform platformTransform)
    {
		border_Left = platformTransform.position.x - platformTransform.localScale.x / 2;
		border_Right = platformTransform.position.x + platformTransform.localScale.x / 2;
	}
}
