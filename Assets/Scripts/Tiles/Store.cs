using UnityEngine;
using System.Collections.Generic;

public class Store : Tile
{
    public const int POWER_AMT = 2;
    public const int COST = 25;
    public const int JOBS = 3;
    public const int GOODS_NEEDED = 5;
    public const int GOODS_COST = 2;

    public const string name = "Store";
    public const string sprite_name = "Sprites/Store";

    public static int num_stores = 0;

    // Use this for initialization
    void Start()
    {
        num_stores++;
        World.instance.ModifyMoney(-COST);
    }

    void OnDestroy()
    {
        num_stores--;
    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(name, sprite_name);
        Store s = obj.AddComponent<Store>();
        obj.transform.position = new Vector3(x, y, 0);
        return s;
    }

    new public static bool CanCreate()
    {
        return World.instance.CanAfford(COST) && World.instance.HasEnoughPower(POWER_AMT);
    }

    public static int EvaluateJobs()
    {
        return num_stores * JOBS;
    }

    public static int EvaluatePowerNeeded()
    {
        return num_stores * POWER_AMT;
    }

    public static int EvaluateGoodsNeeded()
    {
        return num_stores * GOODS_NEEDED;
    }
}
