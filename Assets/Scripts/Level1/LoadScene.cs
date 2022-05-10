using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int NextSceneID;
    public GameObject backgroundImage;
    public GameObject Next;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(NextScene());
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

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(140f);
        Next.SetActive(true);
    }
}
