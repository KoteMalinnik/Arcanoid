using UnityEngine;

public class PlatformEditorResizer : MonoBehaviour
{
	#region Fields

	[SerializeField] float sizeX = 1f;
	[SerializeField] float sizeY = 1f;

    #endregion

    #region Properties

    public float SizeX => sizeX;
    public float SizeY => sizeY;

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
        Log.Message("Изменение размера платформы.");
        transform.localScale = new Vector2(sizeX, sizeY);
    }
}
