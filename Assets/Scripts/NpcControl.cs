using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class NpcControl : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;
    public int WalkDirection;
    public GameObject rotate;
    public GameObject junction;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        if (WalkDirection > 3 || WalkDirection < 0)
        {
            WalkDirection = 0;
        }
        //ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            waitCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    break;
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    break;
                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    break;
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    break;

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("HIT");

        if (collision.tag == "Rotation")
        {
            rotate = GameObject.Find(collision.name);
            WalkDirection = rotate.GetComponent<JunctionRotate>().rotate(WalkDirection);
            transform.position = rotate.transform.position;
            moveSpeed = UnityEngine.Random.Range(2, 5);
        }
        else if (collision.tag == "Junction")
        {
            junction = GameObject.Find(collision.name);
            WalkDirection = junction.GetComponent<JunctionDecision>().decision();
            transform.position = junction.transform.position;
            UnityEngine.Random.Range(2, 5);
        }
    }
}