using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum TileType
    {
        Grass,
        House,
        Store,
        Factory,
        PowerPlant
    }

    private TileType type = TileType.Grass;
    public TileType Type
    {
        get { return type; }
    }

    public const int COST = 35;

    private int position_x;
    private int position_y;

    [SerializeField]
    private GameObject tilePrefab;

    public static Tile CreateNew (int x, int y)
    {
        throw new System.NotImplementedException();
    }

    protected static GameObject MakeObject(string name, string sprite_name)
    {
        // Handles initial setup of Tile gameobject
        GameObject go = new GameObject();
        go.name = name;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>();
        go = SetSprite(go, sprite_name);
        return go;
    }

    protected static GameObject SetSprite(GameObject go, string sprite_name)
    {
        Sprite spr = Resources.Load<Sprite>(sprite_name);
        go.GetComponent<SpriteRenderer>().sprite = spr;
        return go;
    }

    public static Tile CreateNew (int x, int y, TileType type)
    {
        switch(type)
        {
            case TileType.Grass:
                return Grass.CreateNew(x, y);
            case TileType.House:
                return House.CreateNew(x, y);
            case TileType.Store:
                return Store.CreateNew(x, y);
            case TileType.Factory:
                return Factory.CreateNew(x, y);
            case TileType.PowerPlant:
                return PowerPlant.CreateNew(x, y);
            default:
                throw new System.ArgumentException();
        }
    }


    public static bool CanCreate(TileType type)
    {
        switch (type)
        {
            case TileType.Grass:
                return Grass.CanCreate();
            case TileType.House:
                return House.CanCreate();
            case TileType.Store:
                return Store.CanCreate();
            case TileType.Factory:
                return Factory.CanCreate();
            case TileType.PowerPlant:
                return PowerPlant.CanCreate();
            default:
                throw new System.ArgumentException();
        }
    }

}
