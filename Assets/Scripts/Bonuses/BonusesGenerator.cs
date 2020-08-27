using UnityEngine;

public class BonusesGenerator : MonoBehaviour
{
    #region Fields

    [Range(0.0f, 1.0f)]
    [Tooltip("Вероятностный порог появления бонуса. При 1 бонусы не появляются.")]
    [SerializeField] float probabilityThreshold = 0.9f;

    [Space]
    [Range(0.01f, 100f)]
    [SerializeField] float bonusObjectMovementSpeed = 1;

    [Space]
    [SerializeField] string bonusesSpritesFolder = "Assets\\Internal Assets\\Sprites\\Bonuses\\";

    bool generateBonuses = false;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        if (!generateBonuses) return;
        BrickHitController.OnBrickDestroy += CalculateBonusGenerationProbability;
    }

    private void OnDisable()
    {
        if (!generateBonuses) return;
        BrickHitController.OnBrickDestroy -= CalculateBonusGenerationProbability;
    }

    #endregion

    public void GenerateBonusesState(bool state)
    {
        generateBonuses = state;
        Log.Message("Генерация бонусов: " + state);

        if (!generateBonuses) Destroy(gameObject);
    }

    void CalculateBonusGenerationProbability(Vector2 position)
    {
        Log.Message("Рассчет вероятности выпадения бонуса.");

        float probability = Random.Range(0.0f, 1.0f);
        Log.Message($"Вероятность: {probability}. Порог: {probabilityThreshold}.");

        if (probability > probabilityThreshold)
        {
            Log.Message("Порог пройден.");
            GenerateBonus(position);
            return;
        }

        Log.Message("Порог не пройден.");
    }

    void GenerateBonus(Vector2 position)
    {
        Log.Message("Генерация бонуса");

        BonusType bonusType = GetBonusType();
        BonusPlatformTouchController bonusObject = new GameObject($"Bonus_{bonusType}", typeof(BoxCollider2D)).AddComponent<BonusPlatformTouchController>();
        bonusObject.transform.parent = transform;
        bonusObject.transform.position = position;
        SetBonusObjectSprite(bonusObject.gameObject, bonusType);

        Log.Message("Генерация бонуса завершена. Объект создан: " + bonusObject.name);

        bonusObject.Initialize(bonusType, bonusObjectMovementSpeed);
    }

    void SetBonusObjectSprite(GameObject bonusObject, BonusType bonusType)
    {
        var spriteRenderer = bonusObject.AddComponent<SpriteRenderer>();

        Sprite sprite = Resources.Load<Sprite>(bonusesSpritesFolder + bonusType.ToString());
        if (sprite == null)
        {
            Log.Warning("Спрайта бонуса не существует: " + bonusesSpritesFolder + bonusType.ToString() + ".asset");
            sprite = Resources.Load<Sprite>("Assets\\Internal Assets\\Sprites\\Square.asset\\");

            if (sprite == null)
            {
                Log.Error("Спрайта по умолчанию не существует!");
                return;
            }
        }

        spriteRenderer.sprite = sprite;
    }

    BonusType GetBonusType()
    {
        Log.Message("Выбор типа бонуса.");

        int bonusesCount = 4; //Количество разновидностей BonusType
        float bonusProbability = 1 / (float)bonusesCount;

        float probability = Random.Range(0.0f, 1.0f);
        
        BonusType bonusType = BonusType.PlatformWidthIncreasing;

        if (probability < 1 * bonusProbability)
        {
            bonusType = BonusType.AdditionalBall;
        }
        else
        if (probability < 2 * bonusProbability)
        {
            bonusType = BonusType.BallAcceleration;
        }
        else
        if (probability < 3 * bonusProbability)
        {
            bonusType = BonusType.BallSlowdown;
        }

        Log.Message("Тип бонуса: " + bonusType);
        return bonusType;
    }
}
