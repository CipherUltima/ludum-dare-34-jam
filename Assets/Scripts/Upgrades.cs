using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {

    public enum Type {
        TaxIncome,
        GoodsProduction,
        StoreSales,
        PowerPlantOutput
    }

    public static int[] upgrades = new int[4] { 0, 0, 0, 0 };
    public static int[] base_costs = new int[4] { 100, 150, 300, 1000 };
	
    public static void IncreaseLevel(int i, int amount)
    {
        upgrades[i] += amount;
    }

    public static void IncreaseLevel(Type t, int amount)
    {
        IncreaseLevel((int)t, amount);
    }

    public static int GetLevel(int i)
    {
        return upgrades[i];
    }

    public static int GetLevel(Type t)
    {
        return GetLevel((int)t);
    }

    public static int GetCost(int i)
    {
        return (base_costs[i]) + (base_costs[i] * upgrades[i]);
    }

    public static int GetCost(Type t)
    {
        return GetCost((int)t);
    }

}
