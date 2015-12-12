using UnityEngine;

public class PowerPlant : Tile
{
    public const int POWER_AMT = 7;
    public new const int COST = 50;
    public const int JOBS = 5;
    public const int UPKEEP = 5;
    public const string NAME = "PowerPlant";
    public const string SPRITE_NAME = "Sprites/PowerPlant";

    public static int num_plants = 0;

    // Use this for initialization
    void Start()
    {
        type = TileType.PowerPlant;
        num_plants++;
        World.instance.ModifyMoney(-COST);
    }

    void OnDestroy()
    {
        num_plants--;
    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(NAME, SPRITE_NAME);
        PowerPlant p = obj.AddComponent<PowerPlant>();
        obj.transform.position = new Vector3(x, y, 0);
        return p;
    }

    public static bool CanCreate()
    {
        return World.instance.CanAfford(COST);
    }

    public static int EvaluateJobs()
    {
        return num_plants * JOBS;
    }

    public static int EvaluateCosts()
    {
        return num_plants * UPKEEP;
    }

    public static int EvaluatePower(float eff)
    {
        return Mathf.RoundToInt(num_plants * (POWER_AMT + Upgrades.GetLevel(Upgrades.Type.PowerPlantOutput)) * eff);
    }
}
