using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Events

    public static event Action OnGameWin = null;
    public static event Action OnNextLevel = null;

    #endregion

    #region Fields

    [Range(1, 3)]
	[SerializeField] int level = 1;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Log.Message("Уровень: " + level);
        var levelData = Levels.GetLevelData(level);

        BricksGenerator bricksGenerator = GetComponentAtScene<BricksGenerator>();
        bricksGenerator?.Generate(levelData.maxBrickDurability);

        BonusesGenerator bonusesGenerator = GetComponentAtScene<BonusesGenerator>();
        bonusesGenerator?.GenerateBonusesState(levelData.generateBonuses);
    }

    private void OnEnable()
    {
        BricksCounter.OnAllBricksDestroyed += NextLevel;
    }

    private void OnDisable()
    {
        BricksCounter.OnAllBricksDestroyed -= NextLevel;
    }

    #endregion

    void NextLevel()
    {
        Log.Message($"Уровень {level} пройден.");
        
        level++;
        
        if (level > 3)
        {
            Log.Message("Игра пройдена!");
            OnGameWin?.Invoke();
            return;
        }

        Log.Message("Следующий уровень: " + level);
        OnNextLevel?.Invoke();
    }

    T GetComponentAtScene<T>() where T : MonoBehaviour
    {
        T sceneComponent = FindObjectOfType<T>(true);
        if (sceneComponent == null)
        {
            Log.Error($"{typeof(T)} отсутствует на сцене.");
            return null;
        }

        return sceneComponent;
    }
}
