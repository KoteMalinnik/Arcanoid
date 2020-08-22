using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class BallLaunching : MonoBehaviour
{
    #region Fields

    [SerializeField] Vector2 launchDirection = new Vector2(1, 1);
    [SerializeField] KeyCode launchKey = KeyCode.Space;
    bool isLaunched = false;

    Transform platformTransform = null;
    Vector2 launchPosition = Vector2.zero;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        if (launchKey == KeyCode.None) Log.Warning("Клавиша запуска шара не назначена.");

        launchDirection.y = Extencions.MinThreshold(launchDirection.y, 0.5f);
        launchDirection.y = Extencions.MaxThreshold(launchDirection.y, 1f);

        launchDirection.x = Extencions.MinThreshold(launchDirection.x, -1f);
        launchDirection.x = Extencions.MaxThreshold(launchDirection.x, 1f);
    }

    private void Start()
    {
        Log.Message("Ожидание нажатия кнопки запуска шара.");

        platformTransform = FindObjectOfType<PlatformController>().transform;
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
        launchPosition.y = transform.position.y;
        launchPosition.x = platformTransform.position.x;
        transform.position = launchPosition;
    }

    void Launch()
    {
        Log.Message("Запуск шара.");

        var ballMovement = GetComponent<BallMovement>();
        ballMovement.SetDirection(launchDirection);
        ballMovement.AllowMovement();

        isLaunched = true;
        Destroy(this);
    }
}
