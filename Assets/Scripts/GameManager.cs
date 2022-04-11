using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isTutorialFinished;
    public int Step;


    

    // ------------ Game Object lists ----------//
    
    public GameObject Bubble;
    public GameObject TrafficLightObj;
    public GameObject GoStopIndicatorObj;

    public GameObject OverlayObj;

    public GameObject CircleTutObj;
    private GameObject CircleTutObjGenerate;

    public GameObject LeftBlackScreen;
    public GameObject RightBlackScreen;


    // Half Show
    public GameObject HalfShowObj;

    // background object and the fixed 2d on it
    public GameObject BackgroundObj;
    public GameObject FixedObj;
    public GameObject FixedObj2;

    public GameObject BlueLiquidShow;
    public GameObject RedLiquidShow;


    // Buttons
    public GameObject NextButtonObj;
    public GameObject RunButtonObj;

    // ------------ Sprite lists ----------//
    public Sprite LightOn;
    public Sprite LightOff;

    public Sprite HumanGray;
    public Sprite HumanColor;

    public Sprite GreenGo;
    public Sprite RedStop;

    // ------------------------------------//

    public GameObject canv;
    public GameObject highlightcanv;


    public bool Completed;

    private GameObject SpeechBubble;



   // This is level1 stage id settings
   // Stage:0 Tutorial -- Step 1-9: Tutorial Parts
   // Stage:1 


    // Start is called before the first frame update
    void Start()
    {
        Step = -1;
        isTutorialFinished = false;
        HalfShowObj.SetActive(true);
        // When the game starts, automaticlly add a bubble to the scene.
        SpeechBubble = Instantiate(Bubble, new Vector3(-8.38f, -2.08f, 0), Quaternion.identity);
        SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "Welcome students! This is your self driving car. Make sure it has a smooth drive";
        SpeechBubble.transform.SetParent(canv.transform, false);
        RightBlackScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // -- Tutorial every step logic here ---

        // Step 0:

        if (Step == 0)
        {
            RightBlackScreen.SetActive(false);
            LeftBlackScreen.SetActive(true);
            HalfShowObj.SetActive(true);
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(1.00f, 1.00f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "And this is the brain of your car. You will be controlling this section.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;

        }

        if ((Step == 1) && (Completed == false))
        // Disable halfshow and go to original tutorial.
        {
            OverlayObj.SetActive(true);
            HalfShowObj.SetActive(false);
            BackgroundObj.SetActive(true);
            FixedObj.SetActive(true);
            FixedObj2.SetActive(true);
            LeftBlackScreen.SetActive(false);

            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-11.4f, 2.26f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "These are the different sections of the brain.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        

        if ((Step == 2) && (Completed == false))
        // Disable halfshow and go to original tutorial.
        {
            OverlayObj.SetActive(false);
            HalfShowObj.SetActive(false);
            BackgroundObj.SetActive(true);
            FixedObj.SetActive(true);
            FixedObj2.SetActive(true);

            CircleTutObj.SetActive(true);
            CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-12.4f, -0.7f, 0f), Quaternion.identity);
            CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);

            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, -0.01f), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "X1 is the Traffic Light Sensor Input. If X1 = 0, light sensor is off";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        else if ((Step == 3) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(CircleTutObjGenerate);
            CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-12.4f, -0.7f, 0f), Quaternion.identity);
            CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);

            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, -0.01f), Quaternion.identity);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOn;
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "If X1 = 1, light sensor is on and green light is detected.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 4) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(CircleTutObjGenerate);
            CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-10.99f, -1.73f, 0f), Quaternion.identity);
            CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);
            BlueLiquidShow.SetActive(true);

            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            SpeechBubble = Instantiate(Bubble, new Vector3(-9.17f, 0.096f, -0.01f), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "This is the 'neural juice' that forms the input.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 5) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(CircleTutObjGenerate);
            CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-8.66f, -1.669f, 0f), Quaternion.identity);
            CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);

            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            SpeechBubble = Instantiate(Bubble, new Vector3(-6.79f, 0.096f, -0.01f), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "Drag and adjust the weights on the pipes to influence the input and affect the output.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 6) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(CircleTutObjGenerate);
            CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-3.17f, -4.94f, 0f), Quaternion.identity);
            CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);

            RedLiquidShow.SetActive(true);
            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            RunButtonObj.SetActive(true);
            SpeechBubble = Instantiate(Bubble, new Vector3(-1.59f, -3.42f, -0.01f), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "When you click the Run button, the juice color changes based on the weights you assign.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
            isTutorialFinished = true;
        }

        else if ((Step == 7) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(CircleTutObjGenerate);
            //CircleTutObjGenerate = Instantiate(CircleTutObj, new Vector3(-3.17f, -4.94f, 0f), Quaternion.identity);
            //CircleTutObjGenerate.transform.SetParent(highlightcanv.transform, false);


            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            RunButtonObj.SetActive(true);
            SpeechBubble = Instantiate(Bubble, new Vector3(-1.02f, -2.23f, -0.01f), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshPro>().text = "This is called Front Propagation. The color of the juice then decides the output of the network.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
            isTutorialFinished = true;
        }

        else if ((Step == 8) && (Completed == false))
        {
            SceneManager.LoadSceneAsync(2);
        }



        //else if ((Step == 8) && (Completed == false))
        //// Meaning it just starts.
        //{
        //    Destroy(SpeechBubble);
        //    SpeechBubble = Instantiate(Bubble, new Vector3(-1.82f, -4.02f, 0), Quaternion.identity);
        //    SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Whe then brain juice is green, the car goes.";
        //    SpeechBubble.transform.SetParent(canv.transform, false);
        //    GoStopIndicatorObj.GetComponent<SpriteRenderer>().enabled = true;
        //    Completed = true;
        //}



            //else if ((Step == 4) && (Completed == false))
            //// Meaning it just starts.
            //{
            //    Destroy(SpeechBubble);
            //    SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, -2.40f, 0), Quaternion.identity);
            //    SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "X2 is Pedestrain Sensor Input. Now, X2 = 0";
            //    SpeechBubble.transform.SetParent(canv.transform, false);
            //    Completed = true;
            //}

            //else if ((Step == 5) && (Completed == false))
            //// Meaning it just starts.
            //{
            //    Destroy(SpeechBubble);
            //    SpeechBubble = Instantiate(Bubble, new Vector3(-10.56f, -3.46f, 0), Quaternion.identity);
            //    SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "If X2 = 1,  then pedestrains are detected.";
            //    HumanObj.GetComponent<SpriteRenderer>().sprite = HumanColor;
            //    SpeechBubble.transform.SetParent(canv.transform, false);
            //    Completed = true;
            //}

            //else if ((Step == 6) && (Completed == false))
            //// Meaning it just starts.
            //{
            //    Destroy(SpeechBubble);
            //    HumanObj.GetComponent<SpriteRenderer>().sprite = HumanGray;
            //    SpeechBubble = Instantiate(Bubble, new Vector3(-8.44f, -2.52f, 0), Quaternion.identity);
            //    SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Drag W2 plug up and down to control the weight.";
            //    SpeechBubble.transform.SetParent(canv.transform, false);
            //    Completed = true;
            //}

            //else if ((Step == 9) && (Completed == false))
            //// Meaning it just starts.
            //{
            //    Destroy(SpeechBubble);
            //    SpeechBubble = Instantiate(Bubble, new Vector3(-1.82f, -4.02f, 0), Quaternion.identity);
            //    SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "When then brain juice is red, the car will stop.";
            //    SpeechBubble.transform.SetParent(canv.transform, false);
            //    GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = RedStop;
            //    Completed = true;

            //}

            //else if ((Step == 10) && (Completed == false))
            //{
            //    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            //    Destroy(SpeechBubble);
            //    GoStopIndicatorObj.GetComponent<SpriteRenderer>().enabled = false;
            //    NextButtonObj.SetActive(false);
            //    SkipButtonObj.SetActive(false);
            //    // Open Display
            //    isTutorialFinished = true;
            //    // Enable Run Button
            //    //RunButtonObj.SetActive(true);
            //}


    }

}
