using UnityEngine;

public class BricksGenerator : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject brickPrefab = null;

    /// <summary>
    /// Значение позиции по оси Y, ниже которого не будут генерироваться кирпичи.
    /// </summary>
    [SerializeField] float nonGeneratingBoundY = 0;

    [SerializeField] float offsetX = 0.1f;
    [SerializeField] float offsetY = 0.1f;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        offsetX = Extencions.MinThreshold(offsetX, 0);
        offsetY = Extencions.MinThreshold(offsetY, 0);

        nonGeneratingBoundY = Extencions.MinThreshold(nonGeneratingBoundY, -2);
        nonGeneratingBoundY = Extencions.MaxThreshold(nonGeneratingBoundY, Camera.main.orthographicSize);

        Debug.DrawLine(
            new Vector2(-Camera.main.orthographicSize*2, nonGeneratingBoundY),
            new Vector2(Camera.main.orthographicSize*2, nonGeneratingBoundY),
            Color.red, 0.01f);
    }

    #endregion

    public void Generate(int maxDurability)
    {
        maxDurability = (int)Extencions.MinThreshold(maxDurability, 1);

        Vector2 screenTopRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float screenHeight = 2 * screenTopRightPoint.y;
        float brickScaleY = brickPrefab.GetComponent<BoxCollider2D>().size.y;
        float deltaY = offsetY + brickScaleY;

        float allowGeneratingWindow = screenHeight / 2 - nonGeneratingBoundY;
        int linesCount = (int)(allowGeneratingWindow / deltaY);
        float screenSideOffset = (allowGeneratingWindow - (deltaY * linesCount - offsetY)) / 2;

        float generatingPositionY = screenTopRightPoint.y - screenSideOffset - brickScaleY / 2;

        for (int i = 0; i < linesCount; i++)
        {
            CreateLine(generatingPositionY, screenTopRightPoint, maxDurability);
            generatingPositionY -= deltaY;
        }

        Destroy(gameObject);
    }

    void CreateLine(float positionY, Vector2 screenTopRightPoint, int maxDurability)
    {
        float screenWidth = 2 * screenTopRightPoint.x;
        float brickScaleX = brickPrefab.GetComponent<BoxCollider2D>().size.x;
        float deltaX = offsetX + brickScaleX;
        int bricksCountX = (int)(screenWidth / deltaX);
        float screenSideOffset = (screenWidth - (deltaX * bricksCountX - offsetX)) / 2;
        
        Vector2 generatingPosition = Vector2.zero;
        generatingPosition.y = positionY;
        generatingPosition.x = -screenTopRightPoint.x + screenSideOffset + brickScaleX / 2;

        for (int i = 0; i < bricksCountX; i++)
        {
            CreateBrick(generatingPosition, Random.Range(1, maxDurability));
            generatingPosition.x += deltaX;
        }
    }

    void CreateBrick(Vector2 position, int durability)
    {
        Log.Message($"Создание кирпича в позиции {position}.");

        var brickController = Instantiate(brickPrefab, transform).GetComponent<BrickHitController>();
        brickController.Initialize(position, durability);
    }
}
