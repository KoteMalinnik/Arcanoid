using UnityEngine;

public class StaticCollider: MonoBehaviour
{
	#region Fields

	protected BorderData _border = null;

    #endregion

    #region Properties

    public BorderData Border => _border;

	#endregion

	#region MonoBehaviour Callbacks

	protected void Awake()
	{
		Log.Message("Инициализация рамки: " + name);
		_border = new BorderData(transform);
	}

	protected void Update()
    {
#if UNITY_EDITOR
		Vector2 TopLeftPoint = new Vector2(Border.Left, Border.Top);
		Vector2 BottomRightPoint = new Vector2(Border.Right, Border.Bottom);

		Debug.DrawLine(TopLeftPoint, new Vector2(TopLeftPoint.x, BottomRightPoint.y), Color.red);
		Debug.DrawLine(TopLeftPoint, new Vector2(BottomRightPoint.x, TopLeftPoint.y), Color.red);
		Debug.DrawLine(BottomRightPoint, new Vector2(BottomRightPoint.x, TopLeftPoint.y), Color.red);
		Debug.DrawLine(BottomRightPoint, new Vector2(TopLeftPoint.x, BottomRightPoint.y), Color.red);
#endif
	}

	#endregion
}
