using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerate : MonoBehaviour
{
    public GameObject[] balloonsPosition;
    public GameObject[] balloonsGroup;
    public GameObject parentObj;
    private void Start()
    {
        for (int i = 0; i < balloonsPosition.Length; i++)
        {
            Instantiate(balloonsGroup[Random.Range(0, balloonsGroup.Length)], balloonsPosition[i].transform.position, Quaternion.identity, parentObj.transform);
            Destroy(balloonsPosition[i]);
        }
    }
}
