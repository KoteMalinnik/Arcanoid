﻿using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    #region Fields

    [Range(0.0f, 1.0f)]
    [Tooltip("Вероятностный порог появления бонуса. При 1 бонусы не появляются.")]
    [SerializeField] float probabilityThreshold = 0.9f;

    #endregion

    #region Properties

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        BrickController.OnBrickDestroy += CalculateBonusGenerationProbability;
    }

    private void OnDisable()
    {
        BrickController.OnBrickDestroy -= CalculateBonusGenerationProbability;
    }

    private void Start()
    {
        CalculateBonusGenerationProbability(Vector2.zero);
    }

    #endregion

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
        BonusContainer bonusObject = new GameObject($"Bonus_{bonusType}", typeof(BoxCollider2D)).AddComponent<BonusContainer>();
        bonusObject.transform.parent = transform;
        bonusObject.transform.position = position;

        Log.Message("Генерация бонуса завершена. Объект создан: " + bonusObject.name);

        bonusObject.Initialize(bonusType);
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
