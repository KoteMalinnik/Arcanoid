using UnityEngine;

public class BrickController : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Событие, которое вызывается при уничтожении кирпича. Аргументом передается позиция центра кирпича.
    /// </summary>
    public static event System.Action<Vector3> OnBrickDestroy = null;

    #endregion

    #region Fields

    BrickData brickData = null;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
    }

    public void OnDestroy()
    {
        Log.Message("Уничтожение кирпича: " + name);
        OnBrickDestroy?.Invoke(transform.position);
    }

    #endregion

    public void Initialize(Vector2 position, int durability)
    {
        Log.Message($"Инициализация кирпича {name} в позиции {position}.");
        transform.position = position;
        name = durability.ToString();
        brickData = new BrickData(durability, gameObject);
    }

    public void Hit()
    {
        Log.Message($"Удар по кирпичу: {name}.");

        if (brickData.ReduceDurability())
        {
            Destroy(gameObject);
        }
    }
}
