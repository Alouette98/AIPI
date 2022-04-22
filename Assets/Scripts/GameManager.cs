using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isTutorialFinished;
    public int Step;


    

    // ------------ Game Object lists ----------//
    



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
    public GameObject[] images;

    public GameObject[] PlayStartImages;

    public bool Completed;

    public GameObject nextbutton;

    public GameObject nb1;
    public GameObject nb2;

    public GameObject PreviousButton1;
    public GameObject PreviousButton2;

  private int c = 0;



   // This is level1 stage id settings
   // Stage:0 Tutorial -- Step 1-9: Tutorial Parts
   // Stage:1 


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // -- Tutorial every step logic here ---

        // Step 0:

         if (Step == 0)
        {
            images[0].SetActive(true);
            images[1].SetActive(false);
            PreviousButton1.SetActive(false);
        }

        if (Step == 1)
        {
            images[0].SetActive(false);
            images[1].SetActive(true);
            images[2].SetActive(false);
            PreviousButton1.SetActive(true);
        }

        if (Step == 2)
        // Disable halfshow and go to original tutorial.
        {
            images[1].SetActive(false);
            images[2].SetActive(true);
            images[3].SetActive(false);
             
        }
        

        if (Step == 3)
        // Disable halfshow and go to original tutorial.
        {
            images[2].SetActive(false);
            images[3].SetActive(true);
            images[4].SetActive(false);
        }
        if (Step == 4)
        // Meaning it just starts.
        {
            images[3].SetActive(false);
            images[4].SetActive(true);
            images[5].SetActive(false);
        }

        if (Step == 5)
        // Meaning it just starts.
        {
           images[4].SetActive(false);
            images[5].SetActive(true);
            images[6].SetActive(false);
        }

        if (Step == 6)
        // Meaning it just starts.
        {
            images[5].SetActive(false);
            images[6].SetActive(true);
            images[7].SetActive(false);
        }

        if (Step == 7)
        // Meaning it just starts.
        {
            images[6].SetActive(false);
            images[7].SetActive(true);
            images[8].SetActive(false);
        }

        if (Step == 8)
        // Meaning it just starts.
        {
            images[7].SetActive(false);
            images[8].SetActive(true);
            images[9].SetActive(false);
        }

        if (Step == 9)
        // Meaning it just starts.
        {
            images[8].SetActive(false);
            images[9].SetActive(true);
        }
        if (Step == 10)
        // Meaning it just starts.
        {
            SceneManager.LoadScene(3);
        }


        if (Step == 11)
        {

            //here, we should display "LEVEL1" Image first.
            StartCoroutine(PlayStartLevel1());
            Step += 1;
        }

         if (Step == 12)
        {
            images[11].SetActive(true);
           images[12].SetActive(false);
            PreviousButton2.SetActive(false);
        }

        if (Step == 13)
        {
            images[11].SetActive(false);
            images[12].SetActive(true);
            images[13].SetActive(false);
            PreviousButton2.SetActive(true);
        }

        if (Step == 14)
        {
            images[12].SetActive(false);
            images[13].SetActive(true);
            images[14].SetActive(false);
            
        }

        if (Step == 15)
        {
            images[13].SetActive(false);
            images[14].SetActive(true);
            images[15].SetActive(false);
        }

        if (Step == 16)
        {
            images[14].SetActive(false);
            images[15].SetActive(true);
        }
        
        // After this, jump to LEVEL 2, with starting image "LEVEL 2"
        if (Step == 17)
        {
            SceneManager.LoadScene(5);
        }

    }

    public IEnumerator PlayStartLevel1()
    {
        // first temporarly stop next button
        nb1.SetActive(false);
        PlayStartImages[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        nb1.SetActive(true);
        PlayStartImages[0].SetActive(false);
        images[11].SetActive(true);

    }


    public void previous()
    {
        if(Step > 0)
        {
 Step--;
        }
       
    }

    //public IEnumerator PlayStartandJumpToLevel2()
    //{
    //    // first temporarly stop next button
    //    nb2.SetActive(false);
    //    PlayStartImages[1].SetActive(true);
    //    yield return new WaitForSeconds(3f);
    //    nb2.SetActive(true);
    //    PlayStartImages[1].SetActive(false);
    //    SceneManager.LoadScene(5);
    //}

}
