using UnityEngine;

public class RectBorder
{
	#region Fields

	Vector2 _TopLeftPoint = Vector2.zero;

	#endregion

	#region Properties

	public Vector2 TopLeftPoint => _TopLeftPoint;

	#endregion

	public RectBorder(Transform transform)
    {
		UpdateBorder(transform);
	}

	public RectBorder(Vector2 topLeftPoint)
	{
		UpdateBorder(topLeftPoint);
	}

	public void UpdateBorder(Transform transform)
	{
		_TopLeftPoint.x = transform.position.x - transform.localScale.x / 2;
		_TopLeftPoint.y = transform.position.y + transform.localScale.y / 2;
	}

	public void UpdateBorder(Vector2 topLeftPoint)
	{
		_TopLeftPoint = topLeftPoint;
	}
}
