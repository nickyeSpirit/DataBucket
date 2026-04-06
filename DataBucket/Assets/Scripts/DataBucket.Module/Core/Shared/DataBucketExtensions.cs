using JetBrains.Annotations;

public static class DataBucketExtensions
{
    public static string ToLog(this IEntityComponent componentShip)
    {
        if (componentShip == null) return null;
        
        var id = componentShip?.id;
        var level = componentShip?.level;
        var rarity = componentShip?.grade;
        
        return $"{id}:{level}:{(int?)rarity}";
    }
}
