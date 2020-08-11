using UnityEngine;

public class StaticBorder: MonoBehaviour
{
	#region Fields

	protected BorderData _border = null;

    #endregion

    #region Properties

    public BorderData border => _border;

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

#if UNITY_EDITOR
	protected void Update()
    {
		Debug.DrawLine(TopLeftPoint, new Vector2(TopLeftPoint.x, BottomRightPoint.y), Color.red);
		Debug.DrawLine(TopLeftPoint, new Vector2(BottomRightPoint.x, TopLeftPoint.y), Color.red);
		Debug.DrawLine(BottomRightPoint, new Vector2(BottomRightPoint.x, TopLeftPoint.y), Color.red);
		Debug.DrawLine(BottomRightPoint, new Vector2(TopLeftPoint.x, BottomRightPoint.y), Color.red);
	}
#endif

	#endregion
}
