using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Simulation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel2Video()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadLevel2Anim()
    {
        SceneManager.LoadScene(8);
    }

}
