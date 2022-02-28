using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;  

public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update

    public NodeInput n1;
    public NodeInput n2;

    public SpriteRenderer indicator;

    public CalculateB calculateb;
    public int counter;

    public TMPro.TextMeshProUGUI processText;
    public TMPro.TextMeshProUGUI ShoudBeText;


    private int[,] data;

    private int expectedValue;

    void Start()
    {
        data = new int[4, 3] { { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 }, { 1, 1, -1 } };
        counter = 0;
    }


    bool CheckIsMatched()
    {
        if (calculateb.resultValue == expectedValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReadNextData()
    {
        int temp = counter + 1;
        processText.text = temp.ToString() + "/4";
        counter += 1;
        if (counter> 3)
        {
            counter = 0;
        }

        n1.value = data[counter, 0];
        n2.value = data[counter, 1];

        expectedValue = data[counter, 2];
    }



    void Update()
    {
        if (CheckIsMatched())
        {
            indicator.color = new Color(0f, 255f, 0f, 1f);
            ShoudBeText.text = "";
        }
        else
        {
            indicator.color = new Color(255f, 0f, 0f, 1f);
            ShoudBeText.text = "ShouldBe:" + expectedValue.ToString();
        }

        
    }

}
