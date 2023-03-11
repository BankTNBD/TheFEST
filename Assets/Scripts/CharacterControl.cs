using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed;

    public Animator animator;


    private void Update()
    {
        Vector2 dir = Vector2.zero;
        dir.Normalize();
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Animate Index", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Animate Index", 4);
        }
        else
        {
            animator.SetInteger("Animate Index", 0);

        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Animate Index", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Animate Index", 2);
        }
       


        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}