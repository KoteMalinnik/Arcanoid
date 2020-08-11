using UnityEngine;

public class BorderData
{
	#region Fields

	//размер рамки остается неизменным.
	float halfScaleX = 0;
	float halfScaleY = 0;

	Vector2 _TopLeftPoint = Vector2.zero;
	Vector2 _BottomRightPoint = Vector2.zero;

	#endregion

	#region Properties

	public Vector2 TopLeftPoint => _TopLeftPoint;
	public Vector2 BottomRightPoint => _BottomRightPoint;

	#endregion

	public BorderData(Transform transform)
    {
		halfScaleX = transform.localScale.x / 2;
		halfScaleY = transform.localScale.y / 2;

		Update(transform);
	}

	public void Update(Transform transform)
	{
		_TopLeftPoint.x = transform.position.x - halfScaleX;
		_TopLeftPoint.y = transform.position.y + halfScaleY;

		_BottomRightPoint.x = transform.position.x + halfScaleX;
		_BottomRightPoint.y = transform.position.y - halfScaleY;
	}
}
