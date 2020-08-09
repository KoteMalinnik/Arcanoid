using UnityEngine;

public class PlatformSizeController : MonoBehaviour
{
	#region Fields

	[SerializeField] float sizeX = 1f;
	[SerializeField] float sizeY = 1f;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        sizeX = Extencions.MinThreshold(sizeX, 0.05f);
        sizeY = Extencions.MinThreshold(sizeY, 0.05f);

        ChangeSize();
    }

    #endregion

    void ChangeSize()
    {
        transform.localScale = new Vector2(sizeX, sizeY);
    }
}
