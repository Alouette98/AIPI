using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
     public GameObject CreditsLayer;
    public GameObject RoadDisplay;
    public GameObject settingscanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void showSettings()
    {
        // Show settings layer
        CreditsLayer.SetActive(true);
        settingscanvas.SetActive(false);
        // Disable RoadDisplay
        //RoadDisplay.GetComponent<MeshRenderer>().enabled = false;
        settingscanvas.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
