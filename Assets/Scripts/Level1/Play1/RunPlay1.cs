using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlay1 : MonoBehaviour
{

    public PlayOneManager mgr;

    public GameObject hintCanvas;

    public CarBehaviour car;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Play1()
    {
        // Check if it is right

        if (mgr.result <= 0){
            StartCoroutine("errmessage");
        }
        else
        {
            // Play animation


            // Start moving
            car.running = true;
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator errmessage()
    {
        hintCanvas.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        hintCanvas.SetActive(false);
    }
}
