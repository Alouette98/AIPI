using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianBehaviour : MonoBehaviour
{
    //public PlayOneManager mgr;

    public int PedestrianID;

    // Animator
    public Animator pedAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "car")
        {
            Debug.LogWarning("Car hit");

            int randomState = Random.Range(1, 3);
            Debug.LogWarning(randomState);

            pedAnimator.SetInteger("State", randomState);
        }
       
    }


}
