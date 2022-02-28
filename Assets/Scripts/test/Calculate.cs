using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    public float resultValue;
    public NodeInput n1;
    public NodeInput n2;

    public TMPro.TextMeshProUGUI calculateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resultValue = (n1.value * n1.valueWeight) + (n2.value * n2.valueWeight);
        calculateText.text = resultValue.ToString();

    }
}
