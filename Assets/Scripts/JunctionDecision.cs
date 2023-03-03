using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionDecision : MonoBehaviour
{
    public int decisionCount = 4;
    public int[] rotate;
    public int decision()
    {
        return rotate[Random.Range(0, decisionCount)];
    }
}
