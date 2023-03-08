using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
    public GameObject[] obj;
    public GameObject[] genPoints;

    private void Start()
    {
        for (int i = 0; i < genPoints.Length; i++)
        {
            Instantiate(obj[Random.Range(0, obj.Length)], genPoints[i].transform.position, Quaternion.identity);
        }
    }
}
