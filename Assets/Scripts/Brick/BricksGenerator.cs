using UnityEngine;

public class BricksGenerator : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject brickPrefab = null;

    [SerializeField] float offsetX = 0.1f;
    [SerializeField] float offsetY = 0.1f;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        offsetX = Extencions.MinThreshold(offsetX, 0);
        offsetY = Extencions.MinThreshold(offsetY, 0);
    }

    private void Awake()
    {
        Vector2 screenTopRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float screenWidth = 2 * screenTopRightPoint.x;

        float brickScaleX = brickPrefab.transform.localScale.x;
        float deltaX = offsetX + brickScaleX;
        int bricksCountX = (int) (screenWidth / deltaX);

        Vector2 generatingPosition = Vector2.zero;

        float screenOffset = (screenWidth - (deltaX * bricksCountX - offsetX)) / 2;
        generatingPosition.x = -screenTopRightPoint.x + screenOffset + brickScaleX / 2;

        for (int i = 0; i < bricksCountX; i++)
        {
            Debug.DrawRay(generatingPosition, Vector2.up, Color.white, 10);
            generatingPosition.x += deltaX;
        }
    }

    #endregion

    BrickController CreateBreack(Vector2 position, int durablity)
    {
        Log.Message($"Создание кирпича в позиции {position}.");

        var brickController = Instantiate(brickPrefab, transform).GetComponent<BrickController>();
        brickController.Initialize(position, durablity);

        return brickController;
    }
}
