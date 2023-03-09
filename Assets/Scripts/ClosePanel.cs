using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject GameObject;
    bool active;
    public void Close()
    {
        if (active == false) 
        {
            gameObject.transform.gameObject.SetActive(false);
            active = false;
        }
    }
}