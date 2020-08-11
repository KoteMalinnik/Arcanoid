using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class BallLaunching : MonoBehaviour
{
    #region Fields

    [SerializeField] KeyCode launchKey = KeyCode.Space;
    bool isLaunched = false;

    Transform platformTransform = null;
    Vector2 launchPosition = Vector2.zero;
    float offsetY = 0;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        if (launchKey == KeyCode.None) Log.Warning("Клавиша запуска шара не назначена.");
    }

    private void Start()
    {
        Log.Message("Ожидание нажатия кнопки запуска шара.");

        platformTransform = FindObjectOfType<PlatformController>().transform;
        offsetY = platformTransform.localScale.y / 2 + transform.localScale.y / 2;

        UpdatePosition();
    }

    private void Update()
    {
        if (isLaunched) return;

        UpdatePosition();

        if (Input.GetKeyDown(launchKey))
        {
            Log.Message("Кнопка запуска была нажата.");
            Launch();
        }
    }

    #endregion

    void UpdatePosition()
    {
        launchPosition = platformTransform.position;
        launchPosition.y += offsetY;
        transform.position = launchPosition;
    }

    void Launch()
    {
        Log.Message("Запуск шара.");
        
        GetComponent<BallMovement>().AllowMovement();
        isLaunched = true;
        Destroy(this);
    }
}
