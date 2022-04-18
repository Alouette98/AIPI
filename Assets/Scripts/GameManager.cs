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


    public bool Completed;

    public GameObject nextbutton;

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
             if(c == 0)
            {
                nextbutton.transform.position += new Vector3(8f, 0f, 0f);
                c++;
            }

        }

        if (Step == 1)
        {
            images[0].SetActive(false);
            images[1].SetActive(true);
           if(c == 1)
            {
                nextbutton.transform.position += new Vector3(-8f, 0f, 0f);
                c++;
            }

        }

        if (Step == 2)
        // Disable halfshow and go to original tutorial.
        {
            images[1].SetActive(false);
            images[2].SetActive(true);
            if(c == 2)
            {
                nextbutton.transform.position += new Vector3(8f, 0f, 0f);
                c++;
            }
             
        }
        

        if (Step == 3)
        // Disable halfshow and go to original tutorial.
        {
            images[2].SetActive(false);
            images[3].SetActive(true);
        }
        if (Step == 4)
        // Meaning it just starts.
        {
            images[3].SetActive(false);
            images[4].SetActive(true);
        }

        if (Step == 5)
        // Meaning it just starts.
        {
           images[4].SetActive(false);
            images[5].SetActive(true);
        }

        if (Step == 6)
        // Meaning it just starts.
        {
            images[5].SetActive(false);
            images[6].SetActive(true);
        }

        if (Step == 7)
        // Meaning it just starts.
        {
            images[6].SetActive(false);
            images[7].SetActive(true);
        }

        if (Step == 8)
        // Meaning it just starts.
        {
            images[7].SetActive(false);
            images[8].SetActive(true);
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
            images[11].SetActive(true);
        }



        if (Step == 12)
        {
            images[11].SetActive(false);
            images[12].SetActive(true);
        }

        if (Step == 13)
        {
            images[12].SetActive(false);
            images[13].SetActive(true);
        }

        if (Step == 14)
        {
            images[13].SetActive(false);
            images[14].SetActive(true);
        }

        if (Step == 15)
        {
            images[14].SetActive(false);
            images[15].SetActive(true);
        }
        

        if (Step == 16)
        {
            SceneManager.LoadScene(5);
        }

    }

}
