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
        //mgr.clickedTime += 1;
        Debug.Log("Clicked");
        // Disable the brake, let liquid flow
        Brake.SetActive(false);

        mgr.HalfCanvas.SetActive(false);
        mgr.CameraFluid.SetActive(false);
        // After 0 seconds(liquid flow to bottom),
        // show Stop/Go indicator

        if (mgr.result > 0)
        {
            StartCoroutine(mgr.FullScreenPopImage(mgr.GoSprite, 0f, 1.5f, false));
        }
        else
        {
            StartCoroutine(mgr.FullScreenPopImage(mgr.StopSprite, 0f, 1.5f, false));
        }

        mgr.LiquidMixing();


        // Check if it is right
        //if (mgr.result <= 0){
        //    StartCoroutine("errmessage");
        //    //StartCoroutine(mgr.FullScreenPop("ERROR", 1.0f, false)) ;
        //}
        //else
        //{
        //    // Play animation


        //    // Start moving
        //    car.running = true;


        //    // allow going to next level
        //    NextLevelButton.SetActive(true);
        //}

        


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
