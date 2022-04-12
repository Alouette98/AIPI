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
    // Particle list;
    public List<GameObject> particles = new List<GameObject>();

    public GameObject PositiveParticle;
    public GameObject NegativeParticle;
    public bool mixed;

    public bool RunClicked;
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

    void Start()
    {
        RunClicked = false;
        
        hasEnteredCase = false;

        pedAniSpeed = 0.31f;
        StartCoroutine(LevelStart(CaseID, 0f));
        mixed = false;

    }


    public IEnumerator LevelStart(int levelID, float waitTime)

    {
        particleStage = 0;

        yield return new WaitForSeconds(waitTime);

        DisableNextLevelButton();

        //Reenable collider
        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = true;


        // Show fullscreen text:"start level ?"
        startCoroutine = FullScreenPop("Round " + levelID.ToString(), 0f, 1.5f, true, false, true);
        StartCoroutine(startCoroutine);

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
            trafficLogo.GetComponent<SpriteRenderer>().sprite = traffic_gray;
            pedestrianLogo.GetComponent<SpriteRenderer>().sprite = ped_green;

            TubeX1.SetActive(true);
            TubeX2.SetActive(false);

        }else if (CaseID == 3)
        {
            trafficLogo.GetComponent<SpriteRenderer>().sprite = traffic_green;

            TubeX1.SetActive(false);
            TubeX2.SetActive(false);
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
        if (car.gameObject.transform.position.z > 20 && car.gameObject.transform.position.z <= 21)
        {
            CaseID = 2;
            if (!hasEnteredCase)
            {
                StartCoroutine(LevelStart(CaseID, 0f));
            }

        }
        else if (car.gameObject.transform.position.z > 35 && car.gameObject.transform.position.z <= 36)
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

    public IEnumerator FullScreenPop(string TextString, float waitTime, float PopUpTime, bool showHalf, bool nextLevel, bool correct)
    {
        yield return new WaitForSeconds(waitTime);
        DisableHalf();
        
        //yield return new WaitForSeconds(3f);

        FullScreenCanvas.SetActive(true);
        TextOnFullObj.SetActive(true);
        SpriteOnFullObj.SetActive(true);

        TextOnFullObj.GetComponent<TMPro.TextMeshProUGUI>().text = TextString;
        if (TextString.Contains("Round"))
        {
            FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(108f / 256f, 108f / 256f, 108f / 256f, 0.63f);
            SpriteOnFullObj.SetActive(false);
        }
        else
        {
            if (correct)
            {
                FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(50f / 256f, 164f / 256f, 10f / 256f, 0.3f);
                SpriteOnFullObj.GetComponent<SpriteRenderer>().sprite = happy_face;
            }
            else
            {
                FullScreenCanvas.GetComponent<SpriteRenderer>().color = new Color(166f / 256f, 70f / 256f, 70f / 256f, 0.3f);
                SpriteOnFullObj.GetComponent<SpriteRenderer>().sprite = sad_face;
            }
        }

        if (nextLevel)
        {
            //EnableNextLevelButton();
            hasEnteredCase = false;
        }
        yield return new WaitForSeconds(PopUpTime);

        SpriteOnFullObj.SetActive(false);
        TextOnFullObj.SetActive(false);
        FullScreenCanvas.SetActive(false);
        
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
    public void Play1()
    {

        particleStage = 1;
        RunClicked = true;
        
        // remove the collider on valve
        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = false;


        // remove liquid particle
        if (X1 == 1)
        {

            for (int i = 199; i > math.abs(wb1.weightValue) * 100; i--)
            {
                spawner1.WaterDropsObjects[i].SetActive(false);
                //Array.Clear(spawner1.WaterDropsObjects, 0, 1);
            }
            //for (int i = 0; i < 200 ; i++)
            //{
            //    if (spawner1.WaterDropsObjects[i].activeSelf)
            //    {
            //        if (wb1.weightValue > 0)
            //        {
            //            spawner1.WaterDropsObjects[i].GetComponent<MetaballParticleClass>().changeColour(new Color(0f, result / 4.0f * (156f / 256f) + 100f / 256f, 0));
            //        }else if (wb1.weightValue < 0)
            //        {
            //            spawner1.WaterDropsObjects[i].GetComponent<MetaballParticleClass>().changeColour(new Color((-result) / 4.0f * (156f / 256f) + 100f / 256f, 0f, 0));
            //        }
            //    }
            //    //Array.Clear(spawner1.WaterDropsObjects, 0, 1);
            //}

        }

        if (X2 == 1)
        {

            for (int i = 0; i < 200 - math.abs(wb2.weightValue) * 100; i++)
            {
                spawner2.WaterDropsObjects[i].SetActive(false);
                //Array.Clear(spawner1.WaterDropsObjects, 0, 1);
            }
            for (int i = 0; i < 200; i++)
            {
                if (spawner2.WaterDropsObjects[i].activeSelf)
                {
                    if (wb2.weightValue > 0)
                    {
                        spawner2.WaterDropsObjects[i].GetComponent<MetaballParticleClass>().changeColour(new Color(0f, result / 4.0f * (156f / 256f) + 100f / 256f, 0));
                    }
                    else if (wb1.weightValue < 0)
                    {
                        spawner2.WaterDropsObjects[i].GetComponent<MetaballParticleClass>().changeColour(new Color((-result) / 4.0f * (156f / 256f) + 100f / 256f, 0f, 0));
                    }
                }
                //Array.Clear(spawner1.WaterDropsObjects, 0, 1);
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
                StartCoroutine(LoadSceneWithID(12f, 3));
            }
            else if (result == 0)
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("0 in input means disabling the input, and 0 in output confuses Neural Network. Try again!", 5f, 4f, false, false,false));
                StartCoroutine(HalfScreenShow(12f));
            }else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Postive Green means GO. Negative Red means STOP. Now green light is on. Try Again!", 5f, 4f, false, false, false));
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
                StartCoroutine(FullScreenPop("Great Job! Keep on training the network!", 5f, 4f, false, true, true));
            }
            else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Not moving? GREEN light is on!", 5f, 4f, false, false,false));
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
                StartCoroutine(FullScreenPop("Stop! Don't kill people!", 5f, 5f, false, false,false));
                StartCoroutine(LevelStart(CaseID, 8f));
            }
            else if (result == 0)
            {
                //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                StartCoroutine(CarRunning(2f));
                StartCoroutine(FullScreenPop("0 makes Neural Networks confused.Try again!", 5f, 5f, false, false, false));
                StartCoroutine(LevelStart(CaseID, 8f));
            }
            else
            {
                //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                StartCoroutine(FullScreenPop("Thanks! You make our city safer.", 5f, 4f, false, true, true));

                // #TODO NEED UPDATE
                StartCoroutine(LevelStart(3, 10f));
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
                StartCoroutine(LevelStart(CaseID, 12f));
            }
            if ((math.abs(wb1.weightValue) > math.abs(wb2.weightValue)))
            {


                if (wb1.weightValue > 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remeber? Don't kill people!", 5f, 4f, false, false, false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue < 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    
                    StartCoroutine(FullScreenPop("But when green light is on, the car still need to go forward.", 5f, 4f, false, false, false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue > 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Nearly there! But the pedestrain's life is more important than the traffic rules.", 5f, 4f, false, false,false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
                else if (wb1.weightValue < 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remeber? Don't kill people!", 5f, 4f, false, false,false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }
            }

            if ((math.abs(wb1.weightValue) < math.abs(wb2.weightValue)))
            {
                if (wb1.weightValue > 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(FullScreenPop("That's right! The pedestrain's life is more important than the traffic rules. Now the car can always make the right choice!", 5f, 200f, false, true, true));
                    StartCoroutine(StopCar(7f));
                }

                else if (wb1.weightValue < 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(FullScreenPop("But the car still need to go forward when green light is on.", 5f, 4f, false, false,false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

                else if (wb1.weightValue > 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remeber? Don't kill people!", 5f, 4f, false, false, false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

                else if (wb1.weightValue < 0 && wb2.weightValue > 0)
                {
                    //StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
                    StartCoroutine(FullScreenPop("Remeber? Don't kill people! And the car still need to go forward when green light is on.", 5f, 4f, false, false,false));
                    StartCoroutine(LevelStart(CaseID, 12f));
                }

            }
        }

    }


    public void DisableHalf()
    {
        HalfCanvas.SetActive(false);
    }

    public void EnableHalf()
    {
        enableRunButton();
        
        wb1.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        wb2.gameObject.GetComponentInChildren<Collider2D>().enabled = true;


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

    public IEnumerator StopCar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        car.running = false;
    }

    //public 

    public void ResetPedestrian(int PedestrianID)
    {

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
