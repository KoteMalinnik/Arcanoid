using UnityEngine;

public class BorderData
{
	#region Fields

	//размер рамки остается неизменным.
	float halfScaleX = 0;
	float halfScaleY = 0;

	#endregion

	#region Properties

	public float Top { get; private set; } = 0;
	public float Bottom { get; private set; } = 0;
	public float Left { get; private set; } = 0;
	public float Right { get; private set; } = 0;

	#endregion

	#region Constructors

	public BorderData(float Left, float Top, float Right, float Bottom)
    {
		this.Left = Left;
		this.Top = Top;
		this.Right = Right;
		this.Bottom = Bottom;
	}

	public BorderData(Transform transform)
    {
		halfScaleX = transform.localScale.x / 2;
		halfScaleY = transform.localScale.y / 2;

		Update(transform);
	}

	#endregion

	public void Update(Transform transform)
	{
		Left = transform.position.x - halfScaleX;
		Top = transform.position.y + halfScaleY;
		Right = transform.position.x + halfScaleX;
		Bottom = transform.position.y - halfScaleY;
	}
}
