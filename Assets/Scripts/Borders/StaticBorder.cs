using UnityEngine;

public class StaticBorder: MonoBehaviour
{
	#region Fields

	protected BorderData _border = null;

    #endregion

    #region Properties

    public BorderData border => border;

	public Vector2 TopLeftPoint => _border.TopLeftPoint;
	public Vector2 BottomRightPoint => _border.BottomRightPoint;

	#endregion

	#region MonoBehaviour Callbacks

	protected void Awake()
	{
		Log.Message("Инициализация рамки: " + name);
		_border = new BorderData(transform);
		Log.Message($"Левый верхний край: {_border.TopLeftPoint}. Правый нижний край: {_border.BottomRightPoint}.");
	}

    #endregion
}
