using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public int sliderID;

    public Slider slider1;
    public Slider slider2;

    public TMPro.TextMeshProUGUI Text1;
    public TMPro.TextMeshProUGUI Text2;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void UpdateSlider()
    {
        if (sliderID == 1)
        {
            Text1.text = "W1=" + slider1.value;
        }
        else if (sliderID == 2)
        {
            Text1.text = "W1=" + slider1.value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
