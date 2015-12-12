using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Counter : MonoBehaviour {

    public string label = "";
    public string content = "";
    protected string last_content = "";

    protected static List<Counter> all_counters = new List<Counter>();

    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
        all_counters.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (content != last_content)
        {
            text.text = label + ": " + content;
        }

        last_content = content;
	}

    public static Counter GetByLabel (string label)
    {
        foreach(Counter c in all_counters)
        {
            if (c.label == label)
            {
                return c;
            }
        }
        return null;
    }
}
