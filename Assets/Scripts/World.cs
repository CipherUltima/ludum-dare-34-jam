using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    private Tile[,] tiles;

    public static World instance = null;

    private int money = 500;
    private int power_amt = 0;
    private int population = 0;
    private int jobs = 0;
    private int unemployment = 0;
    private int goods = 0;
    private int money_diff = 0;

    private int highscore_money = 0;
    private int highscore_pop = 0;

    [SerializeField]
    private int width = 12;
    [SerializeField]
    private int height = 10;

    private float time_last = 0.0f;

	// Use this for initialization
    void Start()
    {
        instance = this;
        time_last = Time.time;
        highscore_money = PlayerPrefs.GetInt("HighScoreMoney", 0);
        highscore_pop = PlayerPrefs.GetInt("HighScorePopulation", 0);

        tiles = new Tile[width, height];
        GenerateWorld();

        PlaceTile(Tile.TileType.PowerPlant, 5, 5);
        StartCoroutine(SaveHighScores());
	}

    IEnumerator SaveHighScores ()
    {
        for(;;)
        {
            yield return new WaitForSeconds(30.0f);
            PlayerPrefs.Save();
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Time.time > time_last + 0.5f)
        {
            int money_start = money;
            jobs = 0;
            unemployment = 0;
            power_amt = 0;
            // Poll all buildings for happiness, upkeep, etc
            population = House.EvaluatePopulation();

            int power_plant_jobs = PowerPlant.EvaluateJobs();
            float power_plant_eff = 0.0f;
            if (power_plant_jobs != 0)
                power_plant_eff = (population - jobs > power_plant_jobs) ? 1.0f : (Mathf.Max(0.0f, population - jobs) / (float)power_plant_jobs);
            jobs += power_plant_jobs;

            int factory_jobs = Factory.EvaluateJobs();
            float factory_eff = 0.0f;
            if (factory_jobs != 0)
                factory_eff = (population - jobs > factory_jobs) ? 1.0f : (Mathf.Max(0.0f, population - jobs) / (float)factory_jobs);
            jobs += factory_jobs;

            int store_jobs = Store.EvaluateJobs();
            float store_eff = 0.0f;
            if (store_jobs != 0)
                store_eff = (population - jobs > store_jobs) ? 1.0f : (Mathf.Max(0.0f, population - jobs) / (float)store_jobs);
            jobs += store_jobs;

            int power_plant_cost = PowerPlant.EvaluateCosts();
            if (CanAfford(power_plant_cost))
                money -= power_plant_cost;

            power_amt += PowerPlant.EvaluatePower(power_plant_eff);


            int power_needed = Factory.EvaluatePowerNeeded() + Store.EvaluatePowerNeeded() +
                House.EvaluatePowerNeeded();
            float power_eff;
            if (power_needed == 0)
                power_eff = 0.0f;
            else
                power_eff = Mathf.Clamp(power_amt / power_needed, 0.0f, 1.0f);
            factory_eff *= power_eff;
            store_eff *= power_eff;

            power_amt -= power_needed;

            goods += Factory.EvaluateGoods(factory_eff);

            int store_goods = Mathf.RoundToInt(store_eff * Store.EvaluateGoodsNeeded());
            if (goods > store_goods)
            {
                goods -= store_goods;
                money += store_goods * Store.GOODS_COST;
            }

            if (population > jobs)
            {
                unemployment = population - jobs;
            }


            float tax_eff = 0.0f;
            if (population > 0)
                tax_eff = Mathf.Clamp(jobs / (float)population, 0.0f, 1.0f);
            tax_eff *= power_eff;
            money += Mathf.RoundToInt(tax_eff * House.EvaluateTaxIncome());

            money_diff = money - money_start;

            if (money > highscore_money)
            {
                highscore_money = money;
            }
            if (population > highscore_pop)
            {
                highscore_pop = population;
            }
            PlayerPrefs.SetInt("HighScoreMoney", highscore_money);
            PlayerPrefs.SetInt("HighScorePopulation", highscore_pop);

            time_last = Time.time;
        }

        Counter.GetByLabel("Money").content = money.ToString();
        Counter.GetByLabel("Power").content = power_amt.ToString();
        Counter.GetByLabel("Population").content = population.ToString();
        Counter.GetByLabel("Jobs").content = jobs.ToString();
        Counter.GetByLabel("Unemployed").content = unemployment.ToString();
        Counter.GetByLabel("Goods").content = goods.ToString();
        Counter.GetByLabel("Income").content = money_diff.ToString();

        Counter.GetByLabel("Most Money").content = highscore_money.ToString();
        Counter.GetByLabel("Most People").content = highscore_pop.ToString();
	}

    void GenerateWorld ()
    {
        // This should only be run once!!
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = Tile.CreateNew(x, y, Tile.TileType.Grass);
            }
        }
    }

    public bool IsTileAt(int x, int y)
    {
        if (x+1 > width || y+1 > height || x < 0 || y < 0)
            return false;
        if (tiles[x, y] != null)
        {
            return true;
        }
        return false;
    }

    public bool CanPlaceAt(int x, int y)
    {
        return true;
    }

    public void PlaceTile (Tile.TileType type, int x, int y)
    {
        if (x + 1 > width || y + 1 > height || x < 0 || y < 0)
        {
            Debug.LogWarning("Aborting tile placement: Invalid position");

        }
        if (tiles[x, y] != null && type != Tile.TileType.Grass)
        {
            if (tiles[x, y].Type != Tile.TileType.Grass)
            {
                throw new System.InvalidOperationException();
            }
        }
        if (Tile.CanCreate(type))
        {
            DestroyTile(x, y);
            tiles[x, y] = Tile.CreateNew(x, y, type);
        }
    }

    private void DestroyTile (int x, int y)
    {
        Tile t = tiles[x, y];
        Destroy(t.gameObject);
        tiles[x, y] = null;
    }

    public void ModifyPower (int amount)
    {
        power_amt += amount;
    }

    public void ModifyMoney (int amount)
    {
        if (CanAfford(amount))
            money += amount;
    }

    public bool CanAfford (int amount)
    {
        if (money >= amount)
        {
            return true;
        }
        return false;
    }

    public bool HasEnoughPower (int amount)
    {
        amount *= -1;
        if (power_amt >= amount)
        {
            return true;
        }
        return false;
    }

    public void ResetHighScores ()
    {
        highscore_money = 0;
        highscore_pop = 0;
        PlayerPrefs.SetInt("HighScoreMoney", highscore_money);
        PlayerPrefs.SetInt("HighScorePopulation", highscore_pop);
        PlayerPrefs.Save();
    }
}
