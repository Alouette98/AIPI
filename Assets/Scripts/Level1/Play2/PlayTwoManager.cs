using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTwoManager : MonoBehaviour
{
    public float result;
    public WeightBar wb1;
    public GameObject GoStopIndicatorObj;

    public Sprite GoSprite;
    public Sprite StopSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        result = wb1.weightValue * 1f;

        if (result > 0)
        {
            GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = GoSprite;
        }
        else
        {
            GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = StopSprite;
        }
    }
}
