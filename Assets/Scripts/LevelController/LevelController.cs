using UnityEngine;

public class LevelController : MonoBehaviour
{
	#region Fields

	[Range(1, 3)]
	[SerializeField] int level = 1;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Log.Message("Уровень: " + level);
        var levelData = Levels.GetLevelData(level);

        GenerateBricks(levelData);
        GenerateBonusesState(levelData);
    }

    #endregion

    void GenerateBricks(LevelData levelData)
    {
        BricksGenerator bricksGenerator = GetComponentAtScene<BricksGenerator>();
        bricksGenerator?.Generate(levelData.maxBrickDurability);
    }

    void GenerateBonusesState(LevelData levelData)
    {
        BonusesGenerator bonusesGenerator = GetComponentAtScene<BonusesGenerator>();
        bonusesGenerator?.GenerateBonusesState(levelData.generateBonuses);
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
