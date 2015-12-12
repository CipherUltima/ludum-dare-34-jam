using UnityEngine;
using System.Collections;

public class Grass : Tile
{
    public new const int COST = 0;

    public const string NAME = "Grass";
    private static string[] SPRITE_NAMES_HIDDEN = new string[3] 
    {"Sprites/Grass", "Sprites/Grass2", "Sprites/Grass3"};
    public static string SPRITE_NAME {
        get  {
            return SPRITE_NAMES_HIDDEN[Random.Range(0, 3)];
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    new public static Tile CreateNew(int x, int y)
    {
        GameObject obj = Tile.MakeObject(NAME, SPRITE_NAME);
        Grass g = obj.AddComponent<Grass>();
        obj.transform.position = new Vector3(x, y, 0);
        return g;
    }

    public static bool CanCreate()
    {
        return World.instance.CanAfford(COST);
    }
}
