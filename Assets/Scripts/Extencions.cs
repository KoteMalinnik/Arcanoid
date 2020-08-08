using UnityEngine;

public static class Extencions
{
    /// <summary>
    /// Вернет значение <c>Value</c>, если оно превосходит <c>threshold</c>. Иначе вернет <c>threshold</c>.
    /// </summary>
    /// <param name="Value">Проверяемое значение.</param>
    /// <param name="threshold">Минимальный порог.</param>
    /// <returns></returns>
    public static float MinThreshold(float Value, float threshold)
    {
        if (Value < threshold) return threshold;
        return Value;
    }
}
