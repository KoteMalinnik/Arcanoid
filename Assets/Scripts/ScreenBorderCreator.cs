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

        CreateWall("Left", new Vector2(topLeftPoint.x - 0.5f, 0));
        CreateWall("Right", new Vector2(-topLeftPoint.x + 0.5f, 0));
        CreateWall("Top", new Vector2(0, topLeftPoint.y + 0.5f));
        CreateWall("Bottom", new Vector2(0, -topLeftPoint.y - 0.5f));

        Destroy(this);
    }


    void CreateWall(string goName, Vector2 position)
    {
        GameObject wall = new GameObject(goName);
        wall.transform.parent = transform;
        wall.transform.position = position;

        var edgeCollider = wall.AddComponent<EdgeCollider2D>();
    }
}
