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

    public GameObject TextOnFullObj;
    public GameObject SpriteOnFullObj;

    public GameObject CameraFluid;

    public GameObject Brake;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FullScreenPop("Start",0.5f, 2f,true));
    }

    // Update is called once per frame
    void Update()
    {
        result = wb1.weightValue * 1f;

        //if (result > 0)
        //{
        //    GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = GoSprite;
        //}
        //else
        //{
        //    GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = StopSprite;
        //}

    }

    public IEnumerator FullScreenPop(string TextString, float waitTime, float PopUpTime,bool showHalf)
    {
        yield return new WaitForSeconds(waitTime);
        FullScreenCanvas.SetActive(true);
        TextOnFullObj.SetActive(true);
        TextOnFullObj.GetComponent<TMPro.TextMeshProUGUI>().text = TextString;
        yield return new WaitForSeconds(PopUpTime);
        TextOnFullObj.SetActive(false);
        FullScreenCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (showHalf){
            EnableHalf();
            EnableLiquidCamera();
        }
        else
        {
            DisableHalf();
            DisableLiquidCamera();
        }
    }

    public IEnumerator FullScreenPopImage(Sprite image, float waitTime, float PopUpTime, bool showHalf)
    {
        yield return new WaitForSeconds(waitTime);
        FullScreenCanvas.SetActive(true);
        SpriteOnFullObj.SetActive(true);
        SpriteOnFullObj.GetComponent<SpriteRenderer>().sprite = image;
        yield return new WaitForSeconds(PopUpTime);
        SpriteOnFullObj.SetActive(false);
        FullScreenCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (showHalf)
        {
            EnableHalf();
            EnableLiquidCamera();
        }
        else
        {
            DisableHalf();
            DisableLiquidCamera();
        }
    }

    public IEnumerator HalfScreenShow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EnableHalf();
        EnableLiquidCamera();
    }

    public void Play1()
    {

        // Disable the brake, let liquid flow
        Brake.SetActive(false);

        if (result > 0)
        {
            StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));

        }
        else
        {
            StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
            StartCoroutine(FullScreenPop("Fail", 5f, 2f, false));
            StartCoroutine(HalfScreenShow(8f));
        } 

    }


    public void DisableHalf() {
        HalfCanvas.SetActive(false);
    }

    public void EnableHalf()
    {
        HalfCanvas.SetActive(true);
    }

    public void DisableLiquidCamera()
    {
        CameraFluid.SetActive(false);
    }

    public void EnableLiquidCamera()
    {
        CameraFluid.SetActive(true);
    }

}
