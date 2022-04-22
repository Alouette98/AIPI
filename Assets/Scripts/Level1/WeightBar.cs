using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Water2D;

public class WeightBar : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayOneManager mgr;
    private float lastWeightValue;
    public float weightValue;

    public int weightID;

    public TMPro.TextMeshProUGUI WeightText;
    public TMPro.TextMeshProUGUI WeightTextOnValve;


    public bool firstTime;

    public Water2D_Spawner Spawner;

    public GameObject PositiveParticle;
    public GameObject NegativeParticle;

    public List<GameObject> positiveParticles = new List<GameObject>();
    public List<GameObject> negativeParticles = new List<GameObject>();


    public float maxAbsValue;
    public Button minusButton;
    public Button plusButton;

    public GameObject arrow1;
    public GameObject arrow2;

    void Start()
    {
        weightValue = 0;
        firstTime = false;
        //ParticleCheck();
    }

    public void weightPlusOne()
    {
        if (weightValue < maxAbsValue)
        {
            weightValue += 1f;
        }

        if (mgr)
        {
            if (mgr.CaseID == 0)
            {
                arrow1.SetActive(false);
                arrow2.SetActive(true);
            }
        }
        
    }

    public void weightMinusOne()
    {
        if (weightValue > -maxAbsValue)
        {
            weightValue -= 1f;
        }

        if (mgr)
        {
            if (mgr.CaseID == 0)
            {
                arrow1.SetActive(false);
                arrow2.SetActive(true);
            }
        }

    }


    public void ChangeWeight(float weight)
    {
        lastWeightValue = weightValue;
        weightValue = weight;
        firstTime = false;
        //if (mgr != null)
        //{
        //    if ((mgr.X1 == 1 && weightID == 1) || (mgr.X2 == 1 && weightID == 2))
        //    {
        //        if (weightValue - lastWeightValue == 1)
        //        {
        //            // Generate a positive liquid; or if a negative exists, cancel one.
        //            if (negativeParticles.Count != 0)
        //            {
        //                Destroy(negativeParticles[0]);
        //                negativeParticles.RemoveAt(0);
        //            }
        //            else
        //            {
        //                positiveParticles.Add(Instantiate(PositiveParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
        //            }
        //        }
        //        else if (weightValue - lastWeightValue == -1)
        //        {
        //            // Generate a positive liquid; or if a negative exists, cancel one.
        //            if (positiveParticles.Count != 0)
        //            {
        //                Destroy(positiveParticles[0]);
        //                positiveParticles.RemoveAt(0);
        //            }
        //            else
        //            {
        //                negativeParticles.Add(Instantiate(NegativeParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
        //            }
        //        }
        //    }
        //}
    }

    //public void ParticleCheck()
    //{
    //    if (mgr != null)
    //    {
    //        if ((mgr.X1 == 1 && weightID == 1) || (mgr.X2 == 1 && weightID == 2))
    //        {
    //            if (weightValue - (positiveParticles.Count - negativeParticles.Count) != 0)
    //            {
    //                if (weightValue - (positiveParticles.Count - negativeParticles.Count) > 0)
    //                {
    //                    for (int i = 0; i < weightValue - (positiveParticles.Count - negativeParticles.Count); i++)
    //                    {
    //                        positiveParticles.Add(Instantiate(PositiveParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
    //                    }
    //                }
    //                else if (weightValue - (positiveParticles.Count - negativeParticles.Count) < 0)
    //                {
    //                    for (int i = 0; i < -(weightValue - (positiveParticles.Count - negativeParticles.Count)); i++)
    //                    {
    //                        negativeParticles.Add(Instantiate(NegativeParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
    //                    }
    //                }
    //            }
    //            if (positiveParticles.Count > 0 && negativeParticles.Count > 0)
    //            {
    //                Destroy(positiveParticles[0]);
    //                positiveParticles.RemoveAt(0);
    //                Destroy(negativeParticles[0]);
    //                negativeParticles.RemoveAt(0);
    //            }
    //        }
    //    }
    //}


    //public  ParticleCheck()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        if ((mgr.X1 == 1 && weightID == 1) || (mgr.X2 == 1 && weightID == 2))
    //        {
    //            if (weightValue - (positiveParticles.Count - negativeParticles.Count) != 0)
    //            {
    //                if (weightValue - (positiveParticles.Count - negativeParticles.Count) > 0)
    //                {
    //                    for (int i = 0; i < weightValue - (positiveParticles.Count - negativeParticles.Count); i++)
    //                    {
    //                        positiveParticles.Add(Instantiate(PositiveParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
    //                    }
    //                }
    //                else if (weightValue - (positiveParticles.Count - negativeParticles.Count) < 0)
    //                {
    //                    for (int i = 0; i < -(weightValue - (positiveParticles.Count - negativeParticles.Count)); i++)
    //                    {
    //                        negativeParticles.Add(Instantiate(NegativeParticle, this.transform.position + new Vector3(0.7f, 0, 0), Quaternion.identity));
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}


        public void ClearAllParticles()
    {
        positiveParticles.RemoveAll(item => item);
        negativeParticles.RemoveAll(item => item);
    }

    // Update is called once per frame
    void Update()
    {
        if (weightValue == maxAbsValue)
        {
            plusButton.interactable = false;
        }
        else if (weightValue == -maxAbsValue)
        {
            minusButton.interactable = false;
        }
        else
        {
            plusButton.interactable = true;
            minusButton.interactable = true;
        }


        if (!firstTime)
        {
            WeightText.text = "W" + weightID.ToString()+ "=" + weightValue.ToString();
            WeightTextOnValve.text = weightValue.ToString();
        }

        //if (weightValue == 0)
        //{
        //    this.gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        //}
        //else
        //{
        //    this.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        //}
        //if (mgr != null)
        //{
        //    if (!mgr.mixed)
        //    {
        //        ParticleCheck();
        //    }
        }


        //Debug.Log(positiveParticles.Count);
        //Debug.Log(negativeParticles.Count); 


        //if (weightValue != positiveParticles.Count - negativeParticles.Count)
        //{

        //    int delta = (int)weightValue  - (positiveParticles.Count - negativeParticles.Count);
        //    Debug.Log(delta);

        //    while (delta > 0 && positiveParticles.Count > 0){
        //        delta -= 1;
        //        Destroy(positiveParticles[0]);
        //        positiveParticles.RemoveAt(0);
        //    }

        //    while (delta < 0 && negativeParticles.Count > 0)
        //    {
        //        delta += 1;
        //        Destroy(negativeParticles[0]);
        //        negativeParticles.RemoveAt(0);
        //    }

        //    if (delta > 0)
        //    {

        //    }
        //}








    
}
