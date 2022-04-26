using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Level2_Video : MonoBehaviour
{
    //public string url;
    VideoPlayer vidplayer;
    public GameObject videoplayerobject;
    public int c = 1;
    public int b = 0;
    public GameObject simulationbutton;
    public GameObject thumbnail;
    // Start is called before the first frame update
    void Start()
    {
        vidplayer = videoplayerobject.GetComponent<VideoPlayer>();
        //vidplayer.url = url;
        vidplayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "level2new.mp4");
        vidplayer.Pause();
    }

    // Update is called once per frame
    public void Update()
    {
            
    }

    public void PlayRN()
    {
        if(c == 1)
        {
            thumbnail.SetActive(false);
            vidplayer.Play();
            //vidplayer.isLooping = true;
            c++;
            simulationbutton.SetActive(false);
            StartCoroutine(Anim2());
            //b++;
        }

    }

    public IEnumerator Anim2()
    {
        yield return new WaitForSeconds(14f);
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        SceneManager.LoadScene(8);
    }
}
