using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2intro : MonoBehaviour
{
    public GameObject[] PlayStartimages;
    public GameObject hint;

    public GameObject video;

    public GameObject[] halfshow;
    public GameObject nextbutton;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayStartLevel2());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public IEnumerator PlayStartLevel2()
    {
        // first temporarly stop next button
        PlayStartimages[1].SetActive(true);
        yield return new WaitForSeconds(3f);  
        PlayStartimages[1].SetActive(false);
        hint.SetActive(true);
        nextbutton.SetActive(true);

    }

    public void NextButton()
    {
        hint.SetActive(false);
        nextbutton.SetActive(false);
        halfshow[0].SetActive(true);
        halfshow[1].SetActive(true);
        halfshow[2].SetActive(true);
    }
}
