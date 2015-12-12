using UnityEngine;
using System.Collections;

public class Grass : Tile
{
    public const int COST = 0;

    public const string name = "Grass";
    public const string sprite_name = "Sprites/Grass";

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
        GameObject obj = Tile.MakeObject(name, sprite_name);
        Grass g = obj.AddComponent<Grass>();
        obj.transform.position = new Vector3(x, y, 0);
        return g;
    }

    new public static bool CanCreate()
    {
        return World.instance.CanAfford(COST);
    }
}
