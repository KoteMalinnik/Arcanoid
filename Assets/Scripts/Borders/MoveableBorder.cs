using UnityEngine;

public class MoveableBorder: MonoBehaviour
{
	#region Fields

	Border border = null;
	new Transform transform = null;

	#endregion

	#region Properties

	public Vector2 TopLeftPoint => border.TopLeftPoint;
	public Vector2 BottomRightPoint => border.BottomRightPoint;

	#endregion

	#region MonoBehaviour Callbacks

	private void Awake()
	{
		transform = base.transform;
		
		Log.Message("Установка границ: " + name);
		border = new Border();
		border.UpdateBorder(transform);
		Log.Message($"Левый верхний край: {border.TopLeftPoint}. Правый нижний край: {border.BottomRightPoint}.");
	}

    private void Update()
    {
		border.UpdateBorder(transform);
		ScreenBorder.CheckIntersection(this);
	}

    #endregion
}
