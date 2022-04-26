using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainOscillate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //adjust this to change speed
    public float speed = 5f;
    //adjust this to change how high it goes
    public float height = 0.1f;

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed)/2;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, pos.y + (newY * height), pos.z);
    }
}
