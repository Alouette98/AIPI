using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunPlay1 : MonoBehaviour
{

    public PlayOneManager mgr;

    public GameObject hintCanvas;

    public CarBehaviour car;

    public GameObject NextLevelButton;

    public GameObject StopGoIndicator;
    public GameObject Brake;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Play1()
    {
        // Activate stop-go indicator
        StopGoIndicator.SetActive(true);

        // Disable the brake
        Brake.SetActive(false);



        // Check if it is right
        if (mgr.result <= 0){
            StartCoroutine("errmessage");
            //StartCoroutine(FullScreenPop("Start", 1.0f, true));
        }
        else
        {
            // Play animation


            // Start moving
            car.running = true;


            // allow going to next level
            NextLevelButton.SetActive(true);
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator errmessage()
    {
        hintCanvas.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        hintCanvas.SetActive(false);
    }
}
