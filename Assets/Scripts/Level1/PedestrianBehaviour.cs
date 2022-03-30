using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianBehaviour : MonoBehaviour
{
    public PlayOneManager mgr;
    /// <summary>
    /// Pedestrian ID.
    /// </summary>
    public int PedestrianID;
    
    /// <summary>
    // 1 - Walking, but position not change(default status)
    // 2 - Walking, position change
    // 3 - Being hit and lay down
    /// </summary>
    
    public int state;



    // Animator
    private Animator pedAnimator;




    // Transform Velocity
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        pedAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == 1)
        {
            // Do nothing
            //pedAnimator.speed = mgr.pedAniSpeed;
        }
        if (state == 2)
        {
            pedAnimator.speed = mgr.pedAniSpeed;
            this.gameObject.transform.position += new Vector3(0, 0, velocity);
        }
        if (state == 3)
        {
            //StartCoroutine(Die);
            pedAnimator.speed = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "car")
        {
            state = 3;
        }
    }


}
