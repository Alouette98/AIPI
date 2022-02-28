using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonL1 : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextClicked()
    {
        gameManager.Step += 1;
        gameManager.Completed = false;
        Debug.Log(gameManager.Step.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
