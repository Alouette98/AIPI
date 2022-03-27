using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2D;

public class PlayOneManager : MonoBehaviour
{
    // === Numerical ===
    public int X1;
    public int X2;

    // ---- CaseID ----
    public int CaseID;
    public bool hasEnteredCase;

    public float result;
    public WeightBar wb1;
    public WeightBar wb2;

    public TMPro.TextMeshProUGUI EquationText;

    // ---- IEnumerator ----
    public IEnumerator startCoroutine;

    


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
    //public List<Coroutine> coroutineList = new List<Coroutine>();

    void Start()
    {
        hasEnteredCase = false;
        CaseID = 1;
        StartCoroutine(LevelStart(CaseID, 0f));
    }

    public IEnumerator LevelStart(int levelID, float waitTime)

    {
        yield return new WaitForSeconds(waitTime);
        
        DisableNextLevelButton();

        // Show fullscreen text:"start level ?"
        startCoroutine = FullScreenPop("Start" + levelID.ToString(), 0.5f, 2f, true, false);
        StartCoroutine(startCoroutine);
        
        // Let brake reactivate again.
        Brake.SetActive(true);

        // Set X1,X2;
        SetInput();

        // Car move to original position(TODO)
        ResetCarPosition(levelID);

        // Set boolean to true to avoid repeated LevelStart
        hasEnteredCase = true;

        
    }

    void SetInput()
    {
        if (CaseID == 1)
        {
            X1 = 1;
            X2 = 0;
        }else if(CaseID == 2)
        {
            X1 = 0;
            X2 = 1;
        }else if(CaseID == 3)
        {
            X1 = 1;
            X2 = 1;
        }
    }

    void Update()
    {
        // Use car position to identify the CaseID;
        if (car.gameObject.transform.position.z > 20 && car.gameObject.transform.position.z <= 21)
        {
            CaseID = 2;
            if (!hasEnteredCase)
            {
                StartCoroutine(LevelStart(CaseID,0f));
            }

        }else if (car.gameObject.transform.position.z > 35 && car.gameObject.transform.position.z <=36)
        {
            CaseID = 3;
            if (!hasEnteredCase)
            {
                StartCoroutine(LevelStart(CaseID, 0f));
            }
        }

        // Calculate the value result and update it to canvas
        result = wb1.weightValue * X1 + wb2.weightValue * X2;
        EquationText.text = X1.ToString()+" x " + wb1.weightValue.ToString() + " + " + X2.ToString() + " x "+ wb2.weightValue.ToString() +" = " + result.ToString();

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


        // OK, lets check whether the result is correct according to the caseID;
        if (CaseID == 1)
        {
            if (result > 0)
            {
                StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Success!", 5f, 2f, false, true));
            }
            else
            {
                StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Fail", 5f, 2f, false, false));
                StartCoroutine(HalfScreenShow(8f));
            }
        } else if (CaseID == 2)
        {
            if (result > 0)
            {
                // no car shall go so fail
                StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Fail", 5f, 2f, false, false));
                StartCoroutine(LevelStart(CaseID, 7f));
            }
            else
            {
                StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Success!", 5f, 2f, false, true));
                
                // #TODO NEED UPDATE
                StartCoroutine(LevelStart(3, 7f));
                CaseID = 3;

                StartCoroutine(HalfScreenShow(8f));
            }
        }else if (CaseID == 3)
        {

            if (result > 0)
            {
                // no car shall go so fail
                StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Fail", 5f, 2f, false, false));
                StartCoroutine(LevelStart(CaseID, 7f));
            }
            else if (result == 0)
            {
                StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("0 make NN confused", 5f, 2f, true, false));
            }
            else
            {
                StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Success!", 5f, 2f, false, true));
                StartCoroutine(HalfScreenShow(8f));
            }
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
            spawner1.GenerateAndSpawn();
            //spawner2.GenerateAndSpawn();
        } 
        else if (CaseID == 2)
        {
            spawner2.GenerateAndSpawn();
        }
        else if (CaseID == 3)
        {
            spawner1.GenerateAndSpawn();
            spawner2.GenerateAndSpawn();
        }

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

    public void DisableNextLevelButton()
    {
        nextLevelButton.SetActive(false);
    }

    public void ResetCarPosition(int CaseID)
    {
        if (CaseID == 2)
        {
            car.running = false;
            car.transform.position = new Vector3(27.1100006f, -4.27790403f, 23.3939075f);

        }
        else if (CaseID == 3)
        {
            car.running = false;
            car.transform.position = new Vector3(27.1100006f, -4.27790403f, 37.4686317f);
        }

    }

    public IEnumerator ResetCarPosition(float waitTime, int CaseID)
    {
        yield return new WaitForSeconds(waitTime);
        ResetCarPosition(CaseID);
    }

    public void ResetPedestrian(int PedestrianID)
    {
        
    }

}
