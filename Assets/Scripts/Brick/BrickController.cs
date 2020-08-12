using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BrickController : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Событие, которое вызывается при уничтожении кирпича. Аргументом передается позиция центра кирпича.
    /// </summary>
    public static event System.Action<Vector2> OnBrickDestroy = null;

    #endregion

    #region Fields

    [SerializeField] Color oneHitColor = Color.white;
    [SerializeField] Color maxDurabilityColor = Color.yellow;

    BrickData brickData = null;
    SpriteRenderer spriteRenderer = null;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().color = oneHitColor;
    }

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
        name += "Durability: " + durability.ToString();
        brickData = new BrickData(durability, gameObject);

        if (durability > 1)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateColor();
        }
    }

    public void Hit()
    {
        Log.Message($"Удар по кирпичу: {name}.");

        if (brickData.ReduceDurability())
        {
            Destroy(gameObject);
            return;
        }

        UpdateColor();
    }

    void UpdateColor()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.color = Color.Lerp(maxDurabilityColor, oneHitColor, 1 / (float)brickData.durability);
    }
}
