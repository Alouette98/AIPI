using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateB : MonoBehaviour
{

    public float resultValue;
    public NodeInput n1;
    public NodeInput n2;
    
    public TMPro.TextMeshProUGUI calculateBText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resultValue = (n1.value * n1.valueWeight) + (n2.value * n2.valueWeight) + 1f;
        calculateBText.text = "Result:" + resultValue.ToString();

    }
}
