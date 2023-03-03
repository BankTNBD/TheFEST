using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionRotate : MonoBehaviour
{
    public int[] rotation;
    public int rotate (int direction)
    {
        return rotation[direction];
    }
}
