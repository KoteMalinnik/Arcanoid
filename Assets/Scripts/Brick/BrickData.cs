using System;
using UnityEngine;

public class BrickData
{
    #region Properties

    public int durability { get; private set; } = 0;

    #endregion

    public BrickData(int durability, GameObject parent)
    {
        if (parent == null) throw new ArgumentNullException();

        Log.Message($"Установка прочности ({durability}) кирпича {parent.name}.");
        if (durability <= 0)
        {
            Log.Warning($"Значение прочности некорректно. Установка прочности в 1.");
            durability = 1;
        }

        this.durability = durability;
    }

    /// <summary>
    /// Уменьшит прочность кирпича на 1 и вернет true, если прочность стала нулевой.
    /// </summary>
    public bool ReduceDurability()
    {
        durability--;
        return durability <= 0;
    }
}
