using UnityEngine;

public class MouseController : MonoBehaviour
{
    Tile.TileType cursor_type = Tile.TileType.House;

    private bool is_dragging = false;
    private Vector3 mouse_pos_last = new Vector3();

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(world_point.x);
            int y = Mathf.RoundToInt(world_point.y);

            if ((Input.mousePosition.x / Screen.width) <= 0.76f && (Input.mousePosition.x / Screen.width) >= 0.14f)
            {
                if (World.instance.IsTileAt(x, y))
                    World.instance.PlaceTile(cursor_type, x, y);
            }
        }
        if (Input.GetMouseButtonDown(1) && !is_dragging)
        {
            is_dragging = true;
        }
        if (Input.GetMouseButtonUp(1) && is_dragging)
        {
            is_dragging = false;
        }
        if (is_dragging)
        {
            Vector3 delta_pos = Input.mousePosition - mouse_pos_last;
            delta_pos.z = 0;
            delta_pos *= -0.025f;
            Camera.main.transform.Translate(delta_pos);
        }
        mouse_pos_last = Input.mousePosition;
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