using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{

    public GameObject SettingsLayer;
    //public GameObject RoadDisplay;
    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void showSettings()
    {
        // Show settings layer
        SettingsLayer.SetActive(true);
        // Disable RoadDisplay
        //RoadDisplay.GetComponent<MeshRenderer>().enabled = false;

        StartButton.GetComponent<Button>().interactable = false;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
