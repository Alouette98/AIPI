using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    public bool isTutorialFinished;
    public int Step;

    // ------------ Game Object lists ----------//
    
    public GameObject Bubble;
    public GameObject TrafficLightObj;
    public GameObject HumanObj;
    public GameObject GoStopIndicatorObj;

    // Buttons
    public GameObject NextButtonObj;
    public GameObject RunButtonObj;
    public GameObject SkipButtonObj;

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
        Step = 0;
        isTutorialFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        // -- Step logic here ---
        if ((Step == 1) && (Completed == false))
        // Meaning it just starts.
        {
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "X1 is Traffic Lights Sensor Input. If X1 = 0, light sensor is off";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        else if ((Step == 2) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, 0.63f, 0), Quaternion.identity);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOn;
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "If X1 = 1, light sensor is on and green light is detected.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }
        else if ((Step == 3) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            SpeechBubble = Instantiate(Bubble, new Vector3(-8.18f, 0.9f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Drag W1 plug up and down to control the weight.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 7) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            TrafficLightObj.GetComponent<SpriteRenderer>().sprite = LightOff;
            SpeechBubble = Instantiate(Bubble, new Vector3(1.65f, -2.8f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "After you have finished adjusting the weghts, click Run and you will see the results.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 8) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-1.82f, -4.02f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Whe then brain juice is green, the car goes.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            GoStopIndicatorObj.GetComponent<SpriteRenderer>().enabled = true;
            Completed = true;
        }



        else if ((Step == 4) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.55f, -2.40f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "X2 is Pedestrain Sensor Input. Now, X2 = 0";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 5) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-10.56f, -3.46f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "If X2 = 1,  then pedestrains are detected.";
            HumanObj.GetComponent<SpriteRenderer>().sprite = HumanColor;
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 6) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            HumanObj.GetComponent<SpriteRenderer>().sprite = HumanGray;
            SpeechBubble = Instantiate(Bubble, new Vector3(-8.44f, -2.52f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Drag W2 plug up and down to control the weight.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            Completed = true;
        }

        else if ((Step == 9) && (Completed == false))
        // Meaning it just starts.
        {
            Destroy(SpeechBubble);
            SpeechBubble = Instantiate(Bubble, new Vector3(-1.82f, -4.02f, 0), Quaternion.identity);
            SpeechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "When then brain juice is red, the car will stop.";
            SpeechBubble.transform.SetParent(canv.transform, false);
            GoStopIndicatorObj.GetComponent<SpriteRenderer>().sprite = RedStop;
            Completed = true;
            
        }

        else if ((Step == 10) && (Completed == false))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Destroy(SpeechBubble);
            GoStopIndicatorObj.GetComponent<SpriteRenderer>().enabled = false;
            NextButtonObj.SetActive(false);
            SkipButtonObj.SetActive(false);
            // Open Display
            isTutorialFinished = true;
            // Enable Run Button
            //RunButtonObj.SetActive(true);
        }


    }

}
