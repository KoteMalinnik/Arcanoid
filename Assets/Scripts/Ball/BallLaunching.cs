using UnityEngine;

public class BallLaunching : MonoBehaviour
{
    #region Fields

    [SerializeField] KeyCode launchKey = KeyCode.Space;

    bool isLaunched = false;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        if (launchKey == KeyCode.None) Log.Warning("Клавиша запуска шара не назначена.");
    }

    private void Start()
    {
        Log.Message("Ожидание нажатия кнопки запуска шара.");
    }

    private void Update()
    {
        if (isLaunched) return;

        if (Input.GetKeyDown(launchKey))
        {
            Log.Message("Кнопка запуска была нажата.");

            Launch();
        }
    }

    #endregion

    void Launch()
    {
        Log.Message("Запуск шара.");

        var ballMovement = transform.GetComponent<BallMovement>();
        ballMovement.AllowMovement();

        isLaunched = true;
    }
    
    public void Unlaunch()
    {
        isLaunched = false;
    }
}
