using UnityEngine;

public class ScreenBorderCreator : MonoBehaviour
{
    private void Awake()
    {
        Log.Message("Расстановка объектов стен.");
        Vector2 topLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Log.Message($"Координаты углов экрана. Левый верхний: {topLeftPoint}. Правый нижний: {-topLeftPoint}.");

        float width = -2 * topLeftPoint.x;
        float height = 2 * topLeftPoint.y;

        CreateWall("Left", new Vector2(topLeftPoint.x - 0.5f, 0), new Vector2(1, height));
        CreateWall("Right", new Vector2(-topLeftPoint.x + 0.5f, 0), new Vector2(1, height));
        CreateWall("Top", new Vector2(0, topLeftPoint.y + 0.5f), new Vector2(width, 1));
        CreateWall("Bottom", new Vector2(0, -topLeftPoint.y - 0.5f), new Vector2(width, 1));

        Destroy(this);
    }


    void CreateWall(string goName, Vector2 position, Vector2 scale)
    {
        GameObject wall = new GameObject(goName);
        wall.transform.parent = transform;
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.AddComponent<StaticCollider>();
    }
}
