public static class Levels
{
    static readonly LevelData level_1 = new LevelData(1, false);
    static readonly LevelData level_2 = new LevelData(3, false);
    static readonly LevelData level_3 = new LevelData(3, true);

    public static LevelData GetLevelData(int level)
    {
        LevelData levelData = null;

        switch (level)
        {
            case 1:
                levelData = level_1;
                break;
            case 2:
                levelData = level_2;
                break;
            case 3:
                levelData = level_3;
                break;
            default:
                levelData = level_1;
                break;
        }

        return levelData;
    }
}