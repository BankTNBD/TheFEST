using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject GameObject;
    bool active;
    public void OpenAndClose()
    {
        if (active == true) 
        {
             gameObject.transform.gameObject.SetActive(false);
            active = false;
        }
    }
}