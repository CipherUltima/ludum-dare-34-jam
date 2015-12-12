using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColoredCounter : Counter
{

    public Color positive = Color.black;
    public Color negative = Color.red;

    // Use this for initialization
    void Start()
    {
        Counter.all_counters.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (content != last_content)
        {
            text.text = label + ": " + content;
            int result;
            if (int.TryParse(content, out result))
            {
                if (result >= 0)
                {
                    text.color = positive;
                }
                else
                {
                    text.color = negative;
                }
            }
        }

        last_content = content;
    }

    public static new Counter GetByLabel(string label)
    {
        foreach (Counter c in all_counters)
        {
            if (c.label == label)
            {
                return c;
            }
        }
        return null;
    }
}
