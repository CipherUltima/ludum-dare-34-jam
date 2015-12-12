using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinConditionButton : MonoBehaviour {

    private const int WIN_CONDITION_COST = 15000;

    private Button button;
    private Text text;

    private bool purchased = false;

    // Use this for initialization
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        text = gameObject.GetComponentInChildren<Text>();
        button.onClick.AddListener(() => { OnClick(); });

        ResetLabel();
    }

    void FixedUpdate()
    {
        if (!World.instance.CanAfford(WIN_CONDITION_COST) || purchased)
        {
            button.interactable = false;
        }
        else 
        {
            button.interactable = true;
        }
    }

    void OnClick()
    {
        if (World.instance.CanAfford(WIN_CONDITION_COST))
        {
            World.instance.ModifyMoney(-WIN_CONDITION_COST);
            World.instance.TriggerWin();
            purchased = true;
        }
        ResetLabel();
    }

    void ResetLabel()
    {
        text.text = "Emancipation";
        text.text += " - " + WIN_CONDITION_COST.ToString();
        if (purchased)
        {
            text.text = "Emancipation bought!";
        }
    }
}
