using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Intro_Video : MonoBehaviour
{
    public string url;
    VideoPlayer vidplayer;
    // Start is called before the first frame update
    void Start()
    {
        vidplayer = GetComponent<VideoPlayer>();
        vidplayer.url = url;
        
    }

    // Update is called once per frame
    void Update()
    {
        Play();
    }

    void Play()
    {
        if(Input.anyKey)
        {
            vidplayer.Play();
            vidplayer.isLooping = true;
        }
        
    }
}
