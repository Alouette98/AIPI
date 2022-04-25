using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    public GameObject Tooltip;
    private void OnMouseOver()
    {
        Tooltip.SetActive(true);
        Debug.Log("Mouse is over GameObject.");
    }

    private void OnMouseExit()
    {
        Tooltip.SetActive(false);
    }
}
