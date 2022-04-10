using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Water2D;

public class PlayOneManager : MonoBehaviour
{
    // Particle list;
    public List<GameObject> particles = new List<GameObject>();
    public GameObject PositiveParticle;
    public GameObject NegativeParticle;
    public bool mixed;

    // private int clickedTime;
    public int clickedTime;

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


    // -- Liquid Color ---
    private Color defaultWater = new Color(0, 112 / 255f, 255 / 255f, 1);
    private Color RedWater = new Color(200 / 255f, 55 / 255f, 20 / 255f, 1);
    private Color GreenWater = new Color(26 / 255f, 192 / 255f, 73 / 255f);

    public Material waterMaterial;

    //public List<Coroutine> coroutineList = new List<Coroutine>();

    void Start()
    {
        clickedTime = 0;
        hasEnteredCase = false;
        //CaseID = 1;
        pedAniSpeed = 0.31f;
        StartCoroutine(LevelStart(CaseID, 0f));
        mixed = false;

    }

    public IEnumerator LevelStart(int levelID, float waitTime)

    {
        yield return new WaitForSeconds(waitTime);

        DisableNextLevelButton();

        // Show fullscreen text:"start level ?"
        startCoroutine = FullScreenPop("Round " + levelID.ToString(), 0f, 2f, true, false, true);
        StartCoroutine(startCoroutine);

        // Let brake reactivate again.
        Brake.SetActive(true);

        // Set X1,X2;
        SetInput();

        // Car move to original position(TODO)
        ResetCarPosition(levelID);

        // Set boolean to true to avoid repeated LevelStart
        hasEnteredCase = true;
        mixed = false;

        // Clear list
        wb1.ClearAllParticles();
        if (CaseID != 0) { wb2.ClearAllParticles(); }

        // Reset water color
        //SetWaterColor(defaultWater);


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
        // Liquid mixing;
        LiquidMixing();

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

        // Calculate the value result and update it to canvas
        
        
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
        yield return new WaitForSeconds(3f);

        FullScreenCanvas.SetActive(true);
        TextOnFullObj.SetActive(true);
        SpriteOnFullObj.SetActive(true);

        TextOnFullObj.GetComponent<TMPro.TextMeshProUGUI>().text = TextString;
        if (TextString.Contains("Round"))
        {

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
        //SetWaterColor(defaultWater);
    }

    public IEnumerator LoadSceneWithID(float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(id);
    }
    public void Play1()
    {

        // Disable the brake, let liquid flow
        //LiquidMixing();
        Brake.SetActive(false);
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
                    StartCoroutine(CarRunning(2f));
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
                    Debug.Log("==========Correct ======");
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(FullScreenPop("That's right! The pedestrain's life is more important than the traffic rules. Now the car can always make the right choice!", 5f, 200f, false, true, true));
                    StartCoroutine(StopCar(7f));
                }

                else if (wb1.weightValue < 0 && wb2.weightValue < 0)
                {
                    //StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
                    StartCoroutine(CarRunning(2f));
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





            ////---
            //if (result > 0)
            //{
            //    // no car shall go so fail
            //    StartCoroutine(FullScreenPopImage(GoSprite, 0f, 2f, false));
            //    StartCoroutine(CarRunning(2f));
            //    StartCoroutine(FullScreenPop("Fail", 5f, 2f, false, false));
            //    StartCoroutine(LevelStart(CaseID, 7f));
            //}
            //else if (result == 0)
            //{
            //    StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
            //    StartCoroutine(FullScreenPop("0 make Neural Network confused", 5f, 2f, true, false));
            //    StartCoroutine(LevelStart(CaseID, 7f));
            //}
            //else if (result < 0 && wb1.weightValue < 0)
            //{
            //    StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
            //    StartCoroutine(FullScreenPop("Hmm, if no one cross the road later, the car probably won't go...", 5f, 2f, true, false));
            //    StartCoroutine(LevelStart(CaseID, 7f));
            //}
            //else if (result < 0 && wb1.weightValue > 0)
            //{

            //    StartCoroutine(FullScreenPopImage(StopSprite, 0f, 2f, false));
            //    StartCoroutine(FullScreenPop("Right! The pedestrain's life is more important than the traffic rules. But when green light is on, the car can still go forward.", 5f, 200f, false, true));
            //    StartCoroutine(StopCar(7f));

            //}
        }


    }


    public void DisableHalf()
    {
        HalfCanvas.SetActive(false);
        // Clean all particles;

        //if (CaseID == 0)
        //{
        //    for (int i = 1; i < spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length; i++) {
        //        Destroy(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects[i]);
        //    }
        //    Array.Clear(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects, 1, spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length-1);
        //}
        //else if (CaseID == 1)
        //{
        //    for (int i = 1; i < spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length; i++)
        //    {
        //        Destroy(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects[i]);
        //    }
        //    Array.Clear(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects, 1, spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length-1);
        //    //spawner2.GenerateAndSpawn();
        //}
        //else if (CaseID == 2)
        //{
        //    for (int i = 1; i < spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length; i++)
        //    {
        //        Destroy(spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects[i]);
        //    }
        //    Array.Clear(spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects, 1, spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length - 1);
        //}
        //else if (CaseID == 3)
        //{
        //    for (int i = 1; i < spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length; i++)
        //    {
        //        Destroy(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects[i]);
        //    }
        //    Array.Clear(spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects, 1, spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length - 1);
        //    for (int i = 1; i < spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length; i++)
        //    {
        //        Destroy(spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects[i]);
        //    }
        //    Array.Clear(spawner2.GetComponent<Water2D_Spawner>().WaterDropsObjects, 1, spawner1.GetComponent<Water2D_Spawner>().WaterDropsObjects.Length - 1);
        //}

    }

    public void EnableHalf()
    {
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

        //wb1.ParticleCheck();
        //if (CaseID != 0)
        //{
        //    wb2.ParticleCheck();
        //}

        // Reset water material;
        //SetWaterColor(waterMaterial);


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

    public void LiquidMixing()
    {
        // if this is tutorial :
        if (CaseID == 0)
        {
            mixed = true;
            //if (result > 0)
            //{
            //    SetWaterColor(GreenWater);
            //}
            //else if (result < 0)
            //{
            //    SetWaterColor(RedWater);
            //}
        }
        else
        {

            mixed = true;
            Debug.Log("----Mixing Liquid---");
            while (wb1.negativeParticles.Count != 0 && wb2.positiveParticles.Count != 0)
            {
                Debug.Log("-------Step1------");
                Destroy(wb1.negativeParticles[0]);
                wb1.negativeParticles.RemoveAt(0);
                Destroy(wb2.positiveParticles[0]);
                wb2.positiveParticles.RemoveAt(0);
            }

            while (wb1.positiveParticles.Count != 0 && wb2.negativeParticles.Count != 0)
            {
                Debug.Log("-------Step2------");
                Destroy(wb1.positiveParticles[0]);
                wb1.positiveParticles.RemoveAt(0);
                Destroy(wb2.negativeParticles[0]);
                wb2.negativeParticles.RemoveAt(0);
            }

            //if (result > 0)
            //{
            //    SetWaterColor(GreenWater);
            //}
            //else if (result < 0)
            //{
            //    SetWaterColor(RedWater);
            //}
        }
    }

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
