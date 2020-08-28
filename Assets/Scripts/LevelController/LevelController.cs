using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region Fields

    static int Level = 1;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Log.Message("Уровень: " + Level);

        var levelData = Levels.GetLevelData(Level);

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
        Log.Message($"Уровень {Level} пройден.");

        Level++;
        
        if (Level > 3)
        {
            Log.Message("Игра пройдена!");
            Debug.Break();
            return;
        }

        Log.Message("Следующий уровень: " + Level);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Single);
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
