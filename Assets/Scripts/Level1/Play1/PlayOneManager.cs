using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneManager : MonoBehaviour
{

    public float result;
    public WeightBar wb1;
    public GameObject GoStopIndicatorObj;

    public Sprite GoSprite;
    public Sprite StopSprite;


    public GameObject FullScreenCanvas;
    public GameObject HalfCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FullScreenPop("Start",1.0f,true));
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

    IEnumerator FullScreenPop(string TextString, float PopUpTime,bool showHalf)
    {
        FullScreenCanvas.SetActive(true);
        yield return new WaitForSeconds(PopUpTime);
        FullScreenCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (showHalf){
            EnableHalf();
        }
    }

    public void DisableHalf() {
        HalfCanvas.SetActive(false);
    }

    public void EnableHalf()
    {
        HalfCanvas.SetActive(true);
    }

}
