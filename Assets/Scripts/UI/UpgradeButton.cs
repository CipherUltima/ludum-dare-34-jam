using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    private Button button;
    private Text text;

    public Upgrades.Type upgrade_type = Upgrades.Type.TaxIncome;

    private static string[] labels = new string[4] { "Increased Taxation", "Improved Factories", "Advanced Marketing", "More Power!" };

	// Use this for initialization
	void Start () {
        button = gameObject.GetComponent<Button>();
        text = gameObject.GetComponentInChildren<Text>();
        button.onClick.AddListener(() => { OnClick(); });

        ResetLabel();
	}

    void FixedUpdate()
    {
        int cost = Upgrades.GetCost(upgrade_type);
        if (!World.instance.CanAfford(cost))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
	
	void OnClick ()
    {
        int cost = Upgrades.GetCost(upgrade_type);
        if (World.instance.CanAfford(cost))
        {
            World.instance.ModifyMoney(-cost);
            Upgrades.IncreaseLevel(upgrade_type, 1);
        }

        ResetLabel();
    }

    void ResetLabel ()
    {
        text.text = labels[(int)upgrade_type];
        text.text += " " + (Upgrades.GetLevel(upgrade_type)+1).ToString() + " - " + Upgrades.GetCost(upgrade_type).ToString();
    }
}
