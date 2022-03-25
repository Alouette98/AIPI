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

        if (!firstTime)
        {
            WeightText.text = "W" + weightID.ToString()+ "=" + weightValue.ToString();
            if (LevelID == 1)
            {
                EquationText.text = "1 x" + weightValue.ToString() + " + 0 x 0 + 0 = " + weightValue.ToString();
            }
            
        }

        if (weightValue == 0)
        {
            this.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        }



    }
}
