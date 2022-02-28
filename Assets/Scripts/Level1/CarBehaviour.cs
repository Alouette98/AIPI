using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public bool running;

    // Start is called before the first frame update
    void Start()
    {
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {
            this.gameObject.transform.position += new Vector3(0, 0, 0.001f);
        }
    }
}
