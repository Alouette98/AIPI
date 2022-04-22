using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Water2D;


public class PlayOneManager : MonoBehaviour
{
    public bool hasUpdateShown;
    public GameObject infobox;
    public GameObject arrow2;

    // Particle list;
    public List<GameObject> particles = new List<GameObject>();

    public GameObject PositiveParticle;
    public GameObject NegativeParticle;
    public bool mixed;

    public int particleStage;

    // === Numerical ===
    public int X1;
    public int X2;

    // Animation speed
    // -- Ped: 0.31f default
    public float pedAniSpeed;

    // ---- CaseID ----
    public int CaseID;
    public bool hasEnteredCase;
   

    public float result;
    public WeightBar wb1;
    public WeightBar wb2;

    public TMPro.TextMeshProUGUI EquationText;

    // ---- IEnumerator ----
    public IEnumerator startCoroutine;


    // --- Object ---
    public GameObject Ped1;
    public GameObject Ped2;

    public GameObject trafficlight2;

    public GameObject GoStopIndicatorObj;

    public Sprite GoSprite;
    public Sprite StopSprite;


    public GameObject FullScreenCanvas;
    public GameObject HalfCanvas;

    public GameObject TextOnFullObj;
    public GameObject SpriteOnFullObj;

    //public GameObject CameraFluid;

    public GameObject Brake;
    public Water2D_Spawner spawner1;
    public Water2D_Spawner spawner2;

    public GameObject nextLevelButton;

    // ---------- Animation ----------
    public CarBehaviour car;

    public PedestrianBehaviour ped1;
    public PedestrianBehaviour ped2;

    public GameObject trafficLogo;
    public GameObject pedestrianLogo;

    public Sprite traffic_green;
    public Sprite traffic_gray;
    public Sprite ped_green;
    public Sprite ped_gray;

    public Sprite happy_face;
    public Sprite sad_face;

    public GameObject TubeX1;
    public GameObject TubeX2;

    public GameObject RunButton;

    public Material waterMaterial;

    public Sprite[] lvstart_hint;
    public GameObject hintObject;

    public GameObject carspriteOnFullObj;
    public Sprite[] carsprite;
    public GameObject FireworkObj;
    public GameObject newFireworkObj;

    //------Audio-----------
    public AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;

    public GameObject[] PlayStartImages;

    void Start()
    {
        
        hasEnteredCase = false;
        hasUpdateShown = false;
        pedAniSpeed = 0.31f;
        StartCoroutine(LevelStart(CaseID, 0f));
        mixed = false;

    }


    public IEnumerator LevelStart(int levelID, float waitTime)

    {

        // reset liquid stage
        particleStage = 0;

        yield return new WaitForSeconds(waitTime);

        //Reenable collider
        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        wb1.gameObject.GetComponent<Slider>().interactable = true;

        if (CaseID != 0)
        {
            wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            wb1.gameObject.GetComponent<Slider>().interactable = true;
        }


        // Show fullscreen text:"start level ?"
        //startCoroutine = FullScreenPop("Round " + levelID.ToString(), 0f, 1.5f, true, false, true);
        //StartCoroutine(startCoroutine);

        StartCoroutine(Lvstart_lvhint(0f,3f, lvstart_hint[levelID]));

        // Let brake reactivate again. 
        Brake.SetActive(true);

        // Set X1,X2;
        SetInput();

        // Car move to original position
        ResetCarPosition(levelID);

        // Set boolean to true to avoid repeated LevelStart
        hasEnteredCase = true;

        // Detect x1,x2, change traffic sprites, activate and deactivate sliders
        if (CaseID == 2){
            ped1.gameObject.SetActive(true);
            trafficLogo.GetComponent<SpriteRenderer>().sprite = traffic_gray;
            pedestrianLogo.GetComponent<SpriteRenderer>().sprite = ped_green;

            TubeX1.SetActive(true);
            TubeX2.SetActive(false);

            ped1.pedAnimator.SetInteger("State", 0);
        }
        else if (CaseID == 3)
        {
            trafficlight2.SetActive(true);
            ped2.gameObject.SetActive(true);
            trafficLogo.GetComponent<SpriteRenderer>().sprite = traffic_green;

            TubeX1.SetActive(false);
            TubeX2.SetActive(false);

            ped2.pedAnimator.SetInteger("State", 0);
        }

        // Activate Run button
        enableRunButton();
    }

    public void disableRunButton()
    {
        RunButton.GetComponent<Image>().enabled = false;
    }

    public void enableRunButton()
    {
        RunButton.GetComponent<Image>().enabled = true;
    }


    void SetInput()
    {
        if (CaseID == 0)
        {
            X1 = 1;
            X2 = 0;
        }
        if (CaseID == 1)
        {
            X1 = 1;
            X2 = 0;
        }
        else if (CaseID == 2)
        {
            X1 = 0;
            X2 = 1;
        }
        else if (CaseID == 3)
        {
            X1 = 1;
            X2 = 1;
        }
    }

    void Update()
    {

        // Use car position to identify the CaseID;
        if (car.gameObject.transform.position.z > 20 && car.gameObject.transform.position.z <= 20.1)
        {
            CaseID = 2;
            if (!hasEnteredCase)
            {
                StartCoroutine(LevelStart(CaseID, 0f));
            }

        }
        else if (car.gameObject.transform.position.z > 35 && car.gameObject.transform.position.z <= 35.1)
        {
            CaseID = 3;
            if (!hasEnteredCase)
            {
                StartCoroutine(LevelStart(CaseID, 0f));
            }
        }

        // Calculate the value result and update it to canvas real-time
        if (CaseID == 0)
        {
            result = wb1.weightValue;
            EquationText.text = X1.ToString() + " x " + wb1.weightValue.ToString() + "+ 0 = " + result.ToString();
        } 
        else
        {
            result = wb1.weightValue * X1 + wb2.weightValue * X2;
            EquationText.text = X1.ToString() + " x " + wb1.weightValue.ToString() + " + " + X2.ToString() + " x " + wb2.weightValue.ToString() + " = " + result.ToString();
        }

    }


    public IEnumerator disableBrake(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Brake.SetActive(false);
    }

    public IEnumerator Lvstart_lvhint(float waitTime, float PopUpTime, Sprite hintSprite)
    {
        DisableHalf();
        yield return new WaitForSeconds(waitTime);
        hintObject.SetActive(true);
        hintObject.GetComponent<SpriteRenderer>().sprite = hintSprite;
        yield return new WaitForSeconds(PopUpTime);
        hintObject.SetActive(false);
        EnableHalf();
    }



    public IEnumerator FullScreenPop(string TextString, float waitTime, float PopUpTime, bool showHalf, bool nextLevel, bool correct)
    {
        yield return new WaitForSeconds(waitTime);
        DisableHalf();

        FullScreenCanvas.SetActive(true);
        TextOnFullObj.SetActive(true);
        if (CaseID != 0)
        {
            SpriteOnFullObj.SetActive(true);
            newFireworkObj.SetActive(false);
        }

        TextOnFullObj.GetComponent<TMPro.TextMeshProUGUI>().text = TextString;

        // Change canvas background color
        if (TextString.Contains("Round"))
        {
            FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(108f / 256f, 108f / 256f, 108f / 256f, 0.63f);
            SpriteOnFullObj.SetActive(false);
        }
        else
        {
            if (CaseID == 0)
            {
                {
                    if (correct)
                    {
                        carspriteOnFullObj.SetActive(true);
                        FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(74f / 256f, 92f / 256f, 62f / 256f, 0.9f);
                        carspriteOnFullObj.GetComponent<SpriteRenderer>().sprite = carsprite[0];
                        FireworkObj.SetActive(true);
                    }
                    else
                    {
                        carspriteOnFullObj.SetActive(true);
                        FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(51f / 256f, 26f / 256f, 26f / 256f, 0.9f);
                        carspriteOnFullObj.GetComponent<SpriteRenderer>().sprite = carsprite[1];
                    }

                }
            }
            else
            {
                if (correct)
                {
                    //carspriteOnFullObj.SetActive(false);
                    FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(74f / 256f, 92f / 256f, 62f / 256f, 0.9f);
                    SpriteOnFullObj.GetComponent<SpriteRenderer>().sprite = happy_face;
                    newFireworkObj.SetActive(true);
                }
                else
                {
                    //carspriteOnFullObj.SetActive(false);
                    FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(51f / 256f, 26f / 256f, 26f / 256f, 0.9f);
                    SpriteOnFullObj.GetComponent<SpriteRenderer>().sprite = sad_face;
                }
            }
        }

        if (nextLevel)
        {
            //EnableNextLevelButton();
            hasEnteredCase = false;
        }



        yield return new WaitForSeconds(PopUpTime);

        // Well, time to close canvas.
        SpriteOnFullObj.SetActive(false);
        TextOnFullObj.SetActive(false);
        if (CaseID != 0)
        {
            FullScreenCanvas.SetActive(false);

        }
        if (CaseID == 0)
        {
            FullScreenCanvas.SetActive(false);
            carspriteOnFullObj.SetActive(false);
        }        
        yield return new WaitForSeconds(0.5f);

        if (showHalf)
        {
            EnableHalf();
            EnableLiquidCamera();
            //SetWaterColor(defaultWater); 
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
        //SetWaterColor(defaultWater);
    }

    public IEnumerator LoadSceneWithID(float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(id);
    }

    public IEnumerator LiquidFree(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.LogWarning("Free liquid from pipes");
        particleStage = 1;
    }

    public IEnumerator LiquidMixing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.LogWarning("Liquid mixing, color change");
        particleStage = 2;
        
    }

    public void Play1()
    {
        if (CaseID == 0)
        {
            arrow2.SetActive(false);
        }

        StartCoroutine(LiquidFree(0.5f));
        StartCoroutine(LiquidMixing(3f));

        // let output collider disable
        StartCoroutine(disableBrake(2f));


        // remove the collider on valve
        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        if (CaseID != 0)
        {
            wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        }

        // make sliders not interactable

        wb1.gameObject.GetComponentInChildren<Slider>().interactable = false;
        if (CaseID != 0)
        {
            wb2.gameObject.GetComponentInChildren<Slider>().interactable = false;
        }


        // remove liquid particle
        if (X1 == 1)
        {

            for (int i = 199; i > math.abs(wb1.weightValue) * 100; i--)
            {
                spawner1.WaterDropsObjects[i].SetActive(false);
            }

        }

        if (X2 == 1)
        {

            for (int i = 199; i > math.abs(wb2.weightValue) * 100; i--)
            {
                spawner2.WaterDropsObjects[i].SetActive(false);
            }

        }

        disableRunButton();

        // Brake.SetActive(false);


        // OK, lets check whether the result is correct according to the caseID;
        if (CaseID == 0)
        {
            if (result > 0)
            {
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Great Job! Now you know that controlling the weight can decide the output of the Neural Networks!", 5f, 6f, false, true,true));
                PlaySuccess();
                StartCoroutine(LoadSceneWithID(12f, 4));
            }
            else if (result == 0)
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("0 in input means disabling the input, I think our car should give some importance to the green light. Try again!", 5f, 4f, false, false,false));
                PlayFail();
                StartCoroutine(HalfScreenShow(12f));
            }else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Postive Green means GO. Negative Red means STOP. The green light is on right now. Try Again!", 5f, 4f, false, false, false));
                PlayFail();
                StartCoroutine(HalfScreenShow(12f));
                //SetWaterColor(defaultWater);
            }
        }
        else if (CaseID == 1)
        {
            if (result > 0)
            {
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Great Job! The car now knows that the green light is important. Keep on training the network!", 5f, 4f, false, true, true));
                PlaySuccess();
            }
            else if (result == 0)
            {
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                //StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("0 makes Neural Networks confused.Try again!", 5f, 5f, false, false, false));
                PlayFail();
                StartCoroutine(LevelStart(CaseID, 12f));
            }else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Not moving? GREEN light is on!", 5f, 4f, false, false,false));
                PlayFail();
                StartCoroutine(HalfScreenShow(12f));
                //SetWaterColor(defaultWater);
            }
        }
        else if (CaseID == 2)
        {
            if (result > 0)
            {
                // no car shall go so fail
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("Stop! Don't hurt people!", 5f, 5f, false, false,false));
                PlayFail();
                StartCoroutine(LevelStart(CaseID, 12f));
            }
            else if (result == 0)
            {
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("0 makes Neural Networks confused.Try again!", 5f, 5f, false, false, false));
                PlayFail();
                StartCoroutine(LevelStart(CaseID, 12f));
            }
            else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Thanks! The car now knows to stop when it sees a pedestrian. You made our city safer.", 4f, 4f, false, true, true));
                PlaySuccess();
                // #TODO NEED UPDATE
                StartCoroutine(LevelStart(3, 9f));
                CaseID = 3;

                StartCoroutine(HalfScreenShow(12f));
            }
        }
        else if (CaseID == 3)
        {
            if (wb1.weightValue == 0 || wb2.weightValue == 0 || result == 0)
            {
                if (result > 0){
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                }
                else
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                }
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                
                StartCoroutine(FullScreenPop("0 makes Neural Networks confused.Try again!", 5f, 4f, false, false, false));
                PlayFail();
                StartCoroutine(LevelStart(CaseID, 12f));
            }
            if ((math.abs(wb1.weightValue) > math.abs(wb2.weightValue)))
            {


                if (wb1.weightValue > 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remember? Don't kill people!", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue < 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));

                    StartCoroutine(FullScreenPop("Right, the car should stop. However, the car still needs to remember to GO on a green light.", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue > 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Nearly there! But the pedestrain's life is more important than the traffic rules.", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue < 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remember? Don't hurt people!", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
            }

            else if ((math.abs(wb1.weightValue) < math.abs(wb2.weightValue)))
            {
                if (wb1.weightValue > 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(FullScreenPop("That's right! The pedestrain's life is more important than the traffic rules. Now the car can always make the right choice!", 5f, 200f, false, true, true));
                    PlaySuccess();
                    StartCoroutine(StopCar(7f));
                    StartCoroutine(Level2());
                }

                else if (wb1.weightValue < 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(FullScreenPop("But the car still need to go forward when green light is on.", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

                else if (wb1.weightValue > 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remember? Don't hurt people!", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

                else if (wb1.weightValue < 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remember? Don't hurt people! And the car still need to go forward when green light is on.", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
            }
            // NEW ADD CASE
            else if ((math.abs(wb1.weightValue) == math.abs(wb2.weightValue)))
            {
                if (result > 0)
                {
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remember? Don't hurt people! ", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (result < 0)
                {
                    StartCoroutine(FullScreenPop("But the car still need to go forward when green light is on.", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (result ==  0)
                {
                    StartCoroutine(FullScreenPop("0 makes Neural Networks confused.Try again!", 5f, 4f, false, false, false));
                    PlayFail();
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

            }
        }

    }

    public void PlaySuccess()
    {
        audioSource.PlayOneShot(success);
    }

    public void PlayFail()
    {
        audioSource.PlayOneShot(fail);
    }


    public void DisableHalf()
    {
        HalfCanvas.SetActive(false);

        if (X1 == 1 && spawner1.WaterDropsObjects.Length >0 )
        {

            for (int i = 0; i < spawner1.WaterDropsObjects.Length; i++)
            {
                spawner1.WaterDropsObjects[i].SetActive(false);
            }
            
        }

        if (X2== 1 && spawner2.WaterDropsObjects.Length > 0)
        {

            for (int i = 0; i < spawner2.WaterDropsObjects.Length; i++)
            {
                spawner2.WaterDropsObjects[i].SetActive(false);
            }

        }
    }

    public IEnumerator ShowUpdate()
    {
        Time.timeScale = 0;
        infobox.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        infobox.SetActive(false);
        Time.timeScale = 1;
    }

    public void EnableHalf()
    {
        if (CaseID == 1 && !hasUpdateShown)
        {
            StartCoroutine(ShowUpdate());
            hasUpdateShown = true;
        }

        enableRunButton();

        wb1.gameObject.GetComponentInChildren<Slider>().interactable = true;
        if (CaseID != 0)
        {
            wb2.gameObject.GetComponentInChildren<Slider>().interactable = true;
        }



        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        if (CaseID != 0)
        {
            wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        }
        


        HalfCanvas.SetActive(true);

        Brake.SetActive(true);

        if (CaseID == 0)
        {
            spawner1.GenerateAndSpawn();
        }
        else if (CaseID == 1)
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
        //CameraFluid.SetActive(false);
    }

    public void EnableLiquidCamera()
    {
        //CameraFluid.SetActive(true);
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
            car.transform.position = new Vector3(28.757f, -3.861f, 23.3939075f);

        }
        else if (CaseID == 3)
        {
            car.running = false;
            car.transform.position = new Vector3(28.757f, -3.861f, 37.4686317f);
        }

    }

    public IEnumerator ResetCarPosition(float waitTime, int CaseID)
    {
        yield return new WaitForSeconds(waitTime);
        ResetCarPosition(CaseID);
    }

    public IEnumerator StopCar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        car.running = false;
    }

    //public 

    public void ResetPedestrian(int PedestrianID)
    {

    }
    private IEnumerator Level2()
    {
        
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(6);
         PlayStartImages[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        PlayStartImages[1].SetActive(false);
    }
    

    //public void LiquidMixing()
    //{
    //    // if this is tutorial :
    //    if (CaseID == 0)
    //    {
    //        mixed = true;
    //        //if (result > 0)
    //        //{
    //        //    SetWaterColor(GreenWater);
    //        //}
    //        //else if (result < 0)
    //        //{
    //        //    SetWaterColor(RedWater);
    //        //}
    //    }
    //    else
    //    {

    //        mixed = true;
    //        Debug.Log("----Mixing Liquid---");
    //        while (wb1.negativeParticles.Count != 0 && wb2.positiveParticles.Count != 0)
    //        {
    //            Debug.Log("-------Step1------");
    //            Destroy(wb1.negativeParticles[0]);
    //            wb1.negativeParticles.RemoveAt(0);
    //            Destroy(wb2.positiveParticles[0]);
    //            wb2.positiveParticles.RemoveAt(0);
    //        }

    //        while (wb1.positiveParticles.Count != 0 && wb2.negativeParticles.Count != 0)
    //        {
    //            Debug.Log("-------Step2------");
    //            Destroy(wb1.positiveParticles[0]);
    //            wb1.positiveParticles.RemoveAt(0);
    //            Destroy(wb2.negativeParticles[0]);
    //            wb2.negativeParticles.RemoveAt(0);
    //        }

    //        //if (result > 0)
    //        //{
    //        //    SetWaterColor(GreenWater);
    //        //}
    //        //else if (result < 0)
    //        //{
    //        //    SetWaterColor(RedWater);
    //        //}
    //    }
    //}

    //public void SetWaterColor(Color newColor)
    //{
        
    //    if (CaseID == 0)
    //    {
    //        Debug.Log("setcolor!!!!");
    //        spawner1.FillColor = newColor;
    //        spawner1.SetWaterColor(spawner1.FillColor, spawner1.StrokeColor);
    //    }
    //    else
    //    {
    //        if (X1 == 1)
    //        {
    //            spawner1.FillColor = newColor;
    //            spawner1.SetWaterColor(spawner1.FillColor, spawner1.StrokeColor);
    //        }
    //        if (X2 == 1)
    //        {
    //            spawner2.FillColor = newColor;
    //            spawner2.SetWaterColor(spawner2.FillColor, spawner2.StrokeColor);
    //        }
    //    }
    //}

}
