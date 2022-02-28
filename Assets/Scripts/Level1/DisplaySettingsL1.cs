using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettingsL1 : MonoBehaviour
{
    public MeshRenderer mr;

    public GameManager gmr;
    // Start is called before the first frame update
    void Start()
    {
        mr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gmr.isTutorialFinished == true)
        {
            mr.enabled = true;
        }
    }
}
