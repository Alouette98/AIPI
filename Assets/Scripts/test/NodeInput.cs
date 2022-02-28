using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInput : MonoBehaviour
{


    public int value;
    public float valueWeight;
    public SpriteRenderer SR;
    public TMPro.TextMeshProUGUI InputText;
    public TMPro.TextMeshProUGUI WeightText;

    // Start is called before the first frame update
    void Start()
    {
        value = -1;
        valueWeight = 1.0f;    
    }

    public void changeWeight(float weight)
    {
        valueWeight = weight;
    }

    // Update is called once per frame
    void Update()
    {
        if (value != -1){
            InputText.text = "=" + value.ToString();
            WeightText.text = valueWeight.ToString();
        }

        if (value == 1)
        {
            SR.color = new Color(255f, 0f, 0f, 100f);
        }
        else if (value == -1)
        {
            SR.color = new Color(101f, 100f, 99f, 100f);
        }
        else
        {   
            SR.color = new Color(0f, 255f, 0f, 100f);
        }
    }
}
    