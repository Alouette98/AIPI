using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_BackButton : MonoBehaviour
{

    public GameObject SettingsLayer;
    public GameObject RoadDisplay;

    public GameManager gmr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseSettings()
    {
        // Show settings layer
        SettingsLayer.SetActive(false);
        // Disable RoadDisplay
        if (gmr.isTutorialFinished == true){
            RoadDisplay.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
