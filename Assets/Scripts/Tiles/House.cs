using UnityEngine;
using System.Collections.Generic;

public class House : Tile
{
    public const int POWER_AMT = 1;
    public const int COST = 15;
    public const int UPKEEP = 3;
    public const int OCCUPANCY = 5;

    public const string name = "House";
    public const string sprite_name = "Sprites/House";

    public static int num_houses = 0;

    // Use this for initialization
    void Start()
    {
        num_houses++;
        World.instance.ModifyMoney(-COST);
    }

    void OnDestroy()
    {
        num_houses--;
    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(name, sprite_name);
        House h = obj.AddComponent<House>();
        obj.transform.position = new Vector3(x, y, 0);
        return h;
    }

    new public static bool CanCreate()
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
        return num_houses * UPKEEP;
    }
}
