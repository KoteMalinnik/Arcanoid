using UnityEngine;

public static class Extencions
{
    /// <summary>
    /// Вернет значение <c>Value</c>, если оно превосходит <c>threshold</c>. Иначе вернет <c>threshold</c>.
    /// </summary>
    /// <param name="Value">Проверяемое значение.</param>
    /// <param name="threshold">Минимальный порог.</param>
    public static float MinThreshold(float Value, float threshold)
    {
        if (Value < threshold) return threshold;
        return Value;
    }

    /// <summary>
    /// Вернет значение <c>Value</c>, если оно не превосходит <c>threshold</c>. Иначе вернет <c>threshold</c>.
    /// </summary>
    /// <param name="Value">Проверяемое значение.</param>
    /// <param name="threshold">Максимальный порог.</param>
    public static float MaxThreshold(float Value, float threshold)
    {
        if (Value > threshold) return threshold;
        return Value;
    }
}
