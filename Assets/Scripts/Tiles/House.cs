using UnityEngine;
using System.Collections.Generic;

public class House : Tile
{
    public const int POWER_AMT = 1;
    public new const int COST = 15;
    public const int UPKEEP = 3;
    public const int OCCUPANCY = 5;

    public const string NAME = "House";
    public const string SPRITE_NAME = "Sprites/House";

    public static int num_houses = 0;

    // Use this for initialization
    void Start()
    {
        type = TileType.House;
        num_houses++;
        World.instance.ModifyMoney(-COST);
    }

    void OnDestroy()
    {
        num_houses--;
    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(NAME, SPRITE_NAME);
        House h = obj.AddComponent<House>();
        obj.transform.position = new Vector3(x, y, 0);
        return h;
    }

    public static bool CanCreate()
    {
        return World.instance.CanAfford(COST) && World.instance.HasEnoughPower(POWER_AMT);
    }

    public static int EvaluatePopulation()
    {
        return num_houses * OCCUPANCY;
    }

    public static int EvaluatePowerNeeded()
    {
        return num_houses * POWER_AMT;
    }

    public static int EvaluateTaxIncome()
    {
        return num_houses * (UPKEEP + Upgrades.GetLevel(Upgrades.Type.TaxIncome));
    }
}
