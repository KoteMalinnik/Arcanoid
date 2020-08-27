public class LevelData
{
	#region Properties

	public int maxBrickDurability { get; } = 1;
	public bool generateBonuses { get; } = false;

	#endregion

	public LevelData(int maxBrickDurability, bool generateBonuses)
    {
		this.maxBrickDurability = maxBrickDurability > 0 ? maxBrickDurability : 1;
		this.generateBonuses = generateBonuses;
    }
}
