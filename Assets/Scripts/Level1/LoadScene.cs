using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int NextSceneID;
    public GameObject backgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void LoadNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneID);
    }

    public void NextStep()
    {
        backgroundImage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
