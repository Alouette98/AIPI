using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float weightValue;

    public int weightID;

    public TMPro.TextMeshProUGUI WeightText;

    public bool firstTime;
    void Start()
    {
        weightValue = 0;
        firstTime = true;
    }

    public void ChangeWeight(float weight)
    {
        weightValue = weight;
        firstTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstTime)
        {
            WeightText.text = "W" + weightID.ToString()+ "=" + weightValue.ToString();
        }
    }
}
