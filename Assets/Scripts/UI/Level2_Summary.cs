using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Summary : MonoBehaviour
{
    public GameObject nextbutton;
    private int c = 0;

    public GameObject[] summaryimages;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimEnded());
    }

    private IEnumerator AnimEnded()
    {
        yield return new WaitForSeconds(3);
        nextbutton.SetActive(true);
    }

    public void summary()
    {
        if(c==0)
        {
            summaryimages[0].SetActive(true);
            c++;
        }

        else if (c == 1)
        {
            summaryimages[0].SetActive(false);
            summaryimages[1].SetActive(true);
            c++;
        }

        else if(c == 2)
        {
            summaryimages[1].SetActive(false);
            summaryimages[2].SetActive(true);
            c++;
        }
        else if(c == 3)
        {
            summaryimages[2].SetActive(false);
            summaryimages[3].SetActive(true);
            c++;
        }
        else if (c == 4)
        {
            summaryimages[3].SetActive(false);
            summaryimages[4].SetActive(true);
            c++;
        }
        else if(c == 5)
        {
            summaryimages[4].SetActive(false);
            summaryimages[5].SetActive(true);
            c++;
        }
        else if(c == 6)
        {
            summaryimages[5].SetActive(false);
            summaryimages[6].SetActive(true);
            c++;
            nextbutton.SetActive(false);
        }


    }
}
