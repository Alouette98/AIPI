using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_BackButton : MonoBehaviour
{

    public GameObject SettingsLayer;
    //public GameObject RoadDisplay;
<<<<<<< Updated upstream

    //public GameManager gmr;

    public GameObject StartButton;
=======
    public GameObject settingscanvas;
    //public GameManager gmr;
    
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseSettings()
    {
        // Show settings layer
        SettingsLayer.SetActive(false);
        settingscanvas.SetActive(true);
        // Disable RoadDisplay
        //if (gmr.isTutorialFinished == true){
<<<<<<< Updated upstream
        //    RoadDisplay.GetComponent<MeshRenderer>().enabled = true;
        //}

        StartButton.GetComponent<Button>().interactable = true;
=======
            //RoadDisplay.GetComponent<MeshRenderer>().enabled = true;
            
        //}
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
