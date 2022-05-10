using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{

    public GameObject redMuteSign;
    //public GameObject RoadDisplay;
    //public GameObject StartButton;
    private bool mute = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void showSettings()
    {
        // Show settings layer


        if(!mute)
        {
            redMuteSign.SetActive(true);
            mute = true;
            PlayerPrefs.SetFloat("volume", 0f);
            AudioListener.volume = 0f;
        }

        else
        {
            mute = false;
            redMuteSign.SetActive(false);
            PlayerPrefs.SetFloat("volume", 0f);
            AudioListener.volume = 1f;
        }
        
        // Disable RoadDisplay
        //RoadDisplay.GetComponent<MeshRenderer>().enabled = false;

        //StartButton.GetComponent<Button>().interactable = false;

    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
