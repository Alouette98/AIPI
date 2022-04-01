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


    // Half Show
    public GameObject HalfShowObj;

    // background object and the fixed 2d on it
    public GameObject BackgroundObj;
    public GameObject FixedObj;
    public GameObject FixedObj2;


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
        // When the game starts, automaticlly add a bubble to the scene.
        SpeechBubble = Instantiate(Bubble, new Vector3(-5.89f, -2.08f, 0), Quaternion.identity);
        SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Welcome students! You are now the brain of a self driving car.";
        SpeechBubble.transform.SetParent(canv.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        // -- Tutorial every step logic here ---

        // Step 0:

         if (Step == 0)
        {
            HalfShowObj.SetActive(true);
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-5.89f, -2.08f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Left side shows the self driving car and the right side is the brain of the self driving car.";
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

            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.4f, -3.26f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "These are the different sections of the brain";
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

            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "X1 is the Traffic Light Sensor Input. If X1 = 0, light sensor is off";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        else if ((Step == 3) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, 0), Quaternion.identity);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOn;
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "If X1 = 1, light sensor is on and green light is detected.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        else if ((Step == 4) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            SpeechBubble = Instantiate(Bubble, new Vector3(-6.78999996f, 0.439999998f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Drag and adjust the weights on the pipes to control the 'neural juice' and see how it affects your car.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 5) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            RunButtonObj.SetActive(true);
            SpeechBubble = Instantiate(Bubble, new Vector3(-1.59000003f, -3.80999994f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Whenever you click the Run button, the juice flows to the output and this is called Front Propagation.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
            isTutorialFinished = true;
        }

        else if ((Step == 6) && (Completed == false))
        {
            SceneManager.LoadSceneAsync(1);
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
