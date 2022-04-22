using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2intro : MonoBehaviour
{
    public GameObject[] PlayStartimages;
    public GameObject hint;
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
        yield return new WaitForSeconds(5f);
        hint.SetActive(false);
    }
}
