using UnityEngine;

public class MouseController : MonoBehaviour
{
    Tile.TileType cursor_type = Tile.TileType.House;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(worldPoint.x);
            int y = Mathf.RoundToInt(worldPoint.y);

            if (World.instance.IsTileAt(x, y))
                World.instance.PlaceTile(cursor_type, x, y);
        }
    }

    public void SetCursorType(int type)
    {
        SetCursorType((Tile.TileType)type);
    }

    public void SetCursorType(Tile.TileType type)
    {
        cursor_type = type;
    }
}