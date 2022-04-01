using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidChangeColor : MonoBehaviour
{
    public Material WaterMaterial;
    public Material DefaultMaterial;

    public bool passed;
    public PlayOneManager mgr;

    private Color defaultWater = new Color(0, 112 / 255f, 255 / 255f, 1);
    private Color RedWater = new Color(200 / 255f, 55 / 255f, 20 / 255f, 1);
    private Color GreenWater = new Color(26 / 255f, 192 / 255f, 73 / 255f);
    private Color StrokeColor = new Color(4 / 255f, 156 / 255f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        passed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mgr!= null)
        {
            if (passed){
                if (mgr.result > 0)
                {
                    SetWaterColor(GreenWater,StrokeColor);
                }
                if (mgr.result == 0)
                {
                    SetWaterColor(defaultWater, StrokeColor);

                }
                if (mgr.result < 0)
                {
                    SetWaterColor(RedWater, StrokeColor);
                }
            }
             
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "changeColor")
        {
            
            passed = true;

        }
    }




    public void SetWaterColor(Color fill, Color stroke)
    {
        WaterMaterial.SetColor("_Color", fill);
        WaterMaterial.SetColor("_StrokeColor", stroke);

    }


}
