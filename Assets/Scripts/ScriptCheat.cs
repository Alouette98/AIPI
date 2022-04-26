using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCheat : MonoBehaviour
{
    public LoadScene loadScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            loadScript.LoadNextLevel();
        }
    }
}
