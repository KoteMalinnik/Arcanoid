using UnityEngine;

public class BricksCounter : MonoBehaviour
{
    #region Events

    public static event System.Action OnAllBricksDestroyed = null;

    #endregion

    #region Fields

    int bricksCount = 0;

    #endregion

    #region MonoBehabiour Callbacks

    private void OnEnable()
    {
        BrickHitController.OnBrickCreate += AddBrick;
        BrickHitController.OnBrickDestroy += RemoveBrick;
    }

    private void OnDisable()
    {
        BrickHitController.OnBrickCreate -= AddBrick;
        BrickHitController.OnBrickDestroy -= RemoveBrick;
    }

    #endregion

    void AddBrick()
    {
        bricksCount++;
        Log.Message("Добавление кирпича. Количетсво: " + bricksCount);
    }

    void RemoveBrick(Vector2 arg)
    {
        bricksCount--;
        Log.Message("Удаление кирпича. Количетсво: " + bricksCount);

        if (bricksCount <= 0)
        {
            Log.Message("Все кирпичи уничтожены.");
            OnAllBricksDestroyed?.Invoke();
        }
    }
}
