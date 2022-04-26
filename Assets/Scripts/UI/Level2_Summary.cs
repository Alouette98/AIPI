using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Summary : MonoBehaviour
{
    public GameObject nextbutton;
    public GameObject prevbutton;
    bool animationEnded = false;
    private int c = 0;

    public GameObject[] summaryimages;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimEnded());
    }

    private IEnumerator AnimEnded()
    {
        yield return new WaitForSeconds(10);
        animationEnded = true;
        nextbutton.SetActive(true);
    }

    private void Update()
    {
        if (animationEnded)
        {
            if (c == 0)
            {
                prevbutton.SetActive(false);
                summaryimages[0].SetActive(true);
                summaryimages[1].SetActive(false);
            }

            else if (c == 1)
            {
                prevbutton.SetActive(true);
                summaryimages[0].SetActive(false);
                summaryimages[1].SetActive(true);
                summaryimages[2].SetActive(false);
            }

            else if (c == 2)
            {
                summaryimages[1].SetActive(false);
                summaryimages[2].SetActive(true);
                summaryimages[3].SetActive(false);
            }
            else if (c == 3)
            {
                summaryimages[2].SetActive(false);
                summaryimages[3].SetActive(true);
                summaryimages[4].SetActive(false);
            }
            else if (c == 4)
            {
                summaryimages[3].SetActive(false);
                summaryimages[4].SetActive(true);
                summaryimages[5].SetActive(false);
            }
            else if (c == 5)
            {
                summaryimages[4].SetActive(false);
                summaryimages[5].SetActive(true);
                summaryimages[6].SetActive(false);
            }
            else if (c == 6)
            {
                summaryimages[5].SetActive(false);
                summaryimages[6].SetActive(true);
                summaryimages[7].SetActive(false);
            }
            else if (c == 7)
            {
                summaryimages[6].SetActive(false);
                summaryimages[7].SetActive(true);
                nextbutton.SetActive(false);
            }
        }
    }

    public void next()
    {
        c++;
    }

    public void previous()
    {
        c--;
    }

    //----------------------------Deprecated------------------------------
    public void summary()
    {
        if(c==0)
        {
            prevbutton.SetActive(false);
            summaryimages[0].SetActive(true);
            c++;
        }

        else if (c == 1)
        {
            prevbutton.SetActive(true);
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


    public void summary_prev()
    {
        if (c == 0)
        {
            summaryimages[0].SetActive(true);
            prevbutton.SetActive(false);
        }

        else if (c == 1)
        {
            summaryimages[0].SetActive(false);
            summaryimages[1].SetActive(true);
            c--;
        }

        else if (c == 2)
        {
            summaryimages[1].SetActive(false);
            summaryimages[2].SetActive(true);
            c--;
        }
        else if (c == 3)
        {
            summaryimages[2].SetActive(false);
            summaryimages[3].SetActive(true);
            c--;
        }
        else if (c == 4)
        {
            summaryimages[3].SetActive(false);
            summaryimages[4].SetActive(true);
            c--;
        }
        else if (c == 5)
        {
            summaryimages[4].SetActive(false);
            summaryimages[5].SetActive(true);
            c--;
        }
        else if (c == 6)
        {
            summaryimages[5].SetActive(false);
            summaryimages[6].SetActive(true);
            c--;
            
        }


    }

}
