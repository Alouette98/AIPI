using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2D;

public class PlayOneManager : MonoBehaviour
{
    // ---- CaseID ----
    public int CaseID;
    public bool hasEnteredCase;

    // ---- IEnumerator ----
    public IEnumerator startCoroutine;

    
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
    public Water2D_Spawner spawner1;
    public Water2D_Spawner spawner2;

    public GameObject nextLevelButton;

    // ---------- Animation ----------
    public CarBehaviour car;

    //
    public List<Coroutine> coroutineList = new List<Coroutine>();

    void Start()
    {
        hasEnteredCase = false;
        CaseID = 1;
        LevelStart(CaseID);
    }

    void LevelStart(int levelID)
    {
        // Set flag to true to avoid repeated corouotine calling.

        // Show fullscreen text:"start level ?"
        startCoroutine = FullScreenPop("Start" + levelID.ToString(), 0.5f, 2f, true, false);
        StartCoroutine(startCoroutine);
        
        // Let brake reactivate again.
        Brake.SetActive(true);


        // CaseID, spawn water;
        if (CaseID == 2)
        {
            
        }
        
        hasEnteredCase = true;

    }

    void Update()
    {
        // Use car position to identify the CaseID;
        if (car.gameObject.transform.position.z > 20 && car.gameObject.transform.position.z <= 35)
        {
            CaseID = 2;
            if (!hasEnteredCase)
            {
                LevelStart(CaseID);
            }

        }else if (car.gameObject.transform.position.z > 35)
        {
            CaseID = 3;
            if (!hasEnteredCase)
            {
                LevelStart(CaseID);
            }
        }

        // Calculate the value result
        result = wb1.weightValue * 1f;
    }

    public IEnumerator FullScreenPop(string TextString, float waitTime, float PopUpTime,bool showHalf, bool nextLevel)
    {
        yield return new WaitForSeconds(waitTime);
        FullScreenCanvas.SetActive(true);
        TextOnFullObj.SetActive(true);
        TextOnFullObj.GetComponent<TMPro.TextMeshProUGUI>().text = TextString;
        if (nextLevel)
        {
            EnableNextLevelButton();
            hasEnteredCase = false;
        }
        yield return new WaitForSeconds(PopUpTime);
        TextOnFullObj.SetActive(false);
        FullScreenCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        
        if (showHalf){
            EnableHalf();
            EnableLiquidCamera();
        }else
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


    public IEnumerator CarRunning(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        car.running = true;
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
            StartCoroutine(CarRunning(2f));
            StartCoroutine(FullScreenPop("Success!", 5f, 50f, false, true));
        }
        else
        {
            StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
            StartCoroutine(FullScreenPop("Fail", 5f, 2f, false, false));
            StartCoroutine(HalfScreenShow(8f));
        } 

    }


    public void DisableHalf() {
        HalfCanvas.SetActive(false);
    }

    public void EnableHalf()
    {
        HalfCanvas.SetActive(true);
        if (CaseID == 1)
        {
            spawner1.gameObject.SetActive(true);
            coroutineList.Add(spawner1.GenerateAndSpawn());
        } 
        else if (CaseID == 2)
        {
            spawner1.gameObject.SetActive(false);
            spawner2.gameObject.SetActive(true);
            spawner2.GenerateAndSpawn();
        }
        //else if (CaseID == 3)
        //{
        //    spawner1.gameObject.SetActive(true);
        //    spawner1.GenerateAndSpawn();
        //    spawner2.GenerateAndSpawn();
        //}
        
    }

    public void DisableLiquidCamera()
    {
        CameraFluid.SetActive(false);
    }

    public void EnableLiquidCamera()
    {
        CameraFluid.SetActive(true);
    }

    public void EnableNextLevelButton()
    {
        nextLevelButton.SetActive(true);
    }

}
