using UnityEngine;

public class Factory : Tile
{
    public const int POWER_AMT = 3;
    public new const int COST = 35;
    public const int JOBS = 10;
    public const int GOODS = 10;

    public const string NAME = "Factory";
    public const string SPRITE_NAME = "Sprites/Factory";

    public static int num_factories = 0;

    // Use this for initialization
    void Start()
    {
        type = TileType.Factory;
        num_factories++;
        World.instance.ModifyMoney(-COST);
    }

    void OnDestroy()
    {
        num_factories--;
    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(NAME, SPRITE_NAME);
        Factory f = obj.AddComponent<Factory>();
        obj.transform.position = new Vector3(x, y, 0);
        return f;
    }

    public static bool CanCreate()
    {
        return (World.instance.CanAfford(COST) && World.instance.HasEnoughPower(POWER_AMT));
    }

    public static int EvaluateJobs()
    {
        return num_factories * JOBS;
    }

    public static int EvaluateGoods(float eff)
    {
        return Mathf.RoundToInt(num_factories * (GOODS + Upgrades.GetLevel(Upgrades.Type.GoodsProduction)) * eff);
    }

    public static int EvaluatePowerNeeded()
    {
        return num_factories * POWER_AMT;
    }
}
