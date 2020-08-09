﻿using UnityEngine;

public class Border
{
	#region Fields

	Vector2 _TopLeftPoint = Vector2.zero;
	Vector2 _BottomRightPoint = Vector2.zero;

	#endregion

	#region Properties

	public Vector2 TopLeftPoint => _TopLeftPoint;
	public Vector2 BottomRightPoint => _BottomRightPoint;

	#endregion

	public void UpdateBorder(Transform transform)
	{
		_TopLeftPoint.x = transform.position.x - transform.localScale.x / 2;
		_TopLeftPoint.y = transform.position.y + transform.localScale.y / 2;

		_BottomRightPoint.x = transform.position.x + transform.localScale.x / 2;
		_BottomRightPoint.y = transform.position.y - transform.localScale.y / 2;
	}

	public void UpdateBorder(Vector2 topLeftPoint, Vector2 bottomRightPoint)
	{
		_TopLeftPoint = topLeftPoint;
		_BottomRightPoint = bottomRightPoint;
	}
}