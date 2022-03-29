using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // NOTES: Special particle means particles from bottom tube, which has inverse gravity 
    //        before entering mixing bottle.

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > -1.1f)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().gravityScale = -3f;
        }
            
    }
}
