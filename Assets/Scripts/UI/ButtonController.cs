using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour {

    public Button bulldoze;
    public Button house;
    public Button store;
    public Button factory;
    public Button power_plant;

    public Text description_box;

    public int active_button = 0;
    private int prev_button = -1;

    private string[] descriptions;

	// Use this for initialization
	void Start () {
        InitializeText();
	}
	
	// Update is called once per frame
	void Update () {
	    if (active_button != prev_button)
        {
            description_box.text = descriptions[active_button];
            prev_button = active_button;
        }
        switch (active_button)
        {
            case 0:
                bulldoze.Select();
                break;
            case 1:
                house.Select();
                break;
            case 2:
                store.Select();
                break;
            case 3:
                factory.Select();
                break;
            case 4:
                power_plant.Select();
                break;
            default:
                throw new System.ArgumentException();
        }
	}

    void InitializeText()
    {
        descriptions = new string[5];
        descriptions[0] = @"<b><size=""30"">Bulldoze</size></b>

Free.

Deconstructs the chosen tile.";
        descriptions[1] = @"<b><size=""30"">Housing</size></b>

Costs $15.

Requires 1 power.

Houses 5 workers.

Provides $3 in tax income if supplied with jobs and power.";
        descriptions[2] = @"<b><size=""30"">Store</size></b>

Costs $25.

Requires 2 power.

Maximum of 3 workers.

Sells goods produced by factories at $2 per good.";
        descriptions[3] = @"<b><size=""30"">Factory</size></b>

Costs $35.

Requires 3 power.

Maximum of 10 workers.

Does not produce money directly, but manufactures goods which may be sold in stores.";
        descriptions[4] = @"<b><size=""30"">Power Plant</size></b>

Costs $50.

Produces 7 power.

Costs $5 to operate.

Maximum of 5 workers.

Produces power for the city, as long as it's staffed.";
    }

    public void SetActiveButton (int button)
    {
        active_button = button;
    }

}
