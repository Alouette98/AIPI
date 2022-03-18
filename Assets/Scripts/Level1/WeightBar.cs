using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2D;

public class WeightBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float weightValue;

    public int weightID;

    public TMPro.TextMeshProUGUI WeightText;
    public TMPro.TextMeshProUGUI EquationText;

    public bool firstTime;

    public Water2D_Spawner Spawner;

    public int LevelID;
    
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
        //if (weightValue == 0){
        //    Spawner._breakLoop = true;
        //}
        //else
        //{
        //    Spawner._breakLoop = false;
        //}
        if (!firstTime)
        {
            WeightText.text = "W" + weightID.ToString()+ "=" + weightValue.ToString();
            if (LevelID == 1)
            {
                EquationText.text = "1 × " + weightValue.ToString() + " + 0 × 0 + 0 = " + weightValue.ToString();
            }
            
        }


    }
}
