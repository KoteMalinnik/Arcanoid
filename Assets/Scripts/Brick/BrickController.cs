using UnityEngine;

public class BrickController : MonoBehaviour
{
	#region Fields

	int durability = 1;

    #endregion

    #region Properties

    int Durability
    {
        get { return durability; }
        set
        {
            durability = value;
            if (value <= 0) OnBrickDestroy();
        }
    }

    #endregion
    
    public void SetLifes(int durability)
    {
        Log.Message($"Установка прочности ({durability}) кирпича {name}.");
        if (durability <= 0)
        {
            Log.Warning($"Значение прочности некорректно. Установка прочности в 1.");
            durability = 1;
        }

        this.durability = durability;
    }

    public void Hit()
    {
        Log.Message($"Удар по кирпичу: {name}. Прочность: {Durability - 1}.");
        Durability--;
    }

    public void OnBrickDestroy()
    {
        Log.Message("Уничтожение кирпича: " + name);
    }
}
