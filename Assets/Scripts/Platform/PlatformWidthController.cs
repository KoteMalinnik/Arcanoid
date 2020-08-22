using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlatformWidthController : MonoBehaviour
{
	#region Fields

	[SerializeField] float increasingWidthStep = 1;
	[SerializeField] float maxWidth = 6;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
	{
		BonusController.OnBonusReceive += IncreaseWidth;
	}

	private void OnDisable()
	{
		BonusController.OnBonusReceive -= IncreaseWidth;
	}

    #endregion

	void IncreaseWidth(BonusType bonusType)
    {
		if (bonusType != BonusType.PlatformWidthIncreasing) return;

		Log.Message("Увеличение ширины платформы.");
		
		var spriteRenderer = GetComponent<SpriteRenderer>();
		Vector2 size = spriteRenderer.size;

		if (size.x + increasingWidthStep > maxWidth)
        {
			Log.Message("Платформа не может стать больше.");
			return;
        }

		size.x += increasingWidthStep;

		spriteRenderer.size = size;
		GetComponent<BoxCollider2D>().size = size;
    }
}
