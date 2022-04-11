using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public bool running;
    public float velocity;
    private float firstCheckpoint;
    private float secondCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        running = false;
        firstCheckpoint = 25f;
        secondCheckpoint = 37f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (running == true)
        {
            this.gameObject.transform.position += new Vector3(0, 0, velocity);
        }
    }
}
