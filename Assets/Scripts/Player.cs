using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    Vector3 moveDir;

    float moveSpeed = 5f;
    float jumpForce = 5f;
    float verticalVelocity;

    bool isJump;
    bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();
        moving();
    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("Ground")); //this int exactly different general int value. maybe checksome Layer gab.
        if (ray) { 
            
        }
    }

    private void moving() {
       moveDir.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir * jumpForce;
        anim.SetBool("Walk", moveDir.x != 0.0f);

        if (moveDir.x != 0.0f) { //go to right then x=1, scale x = -1, if left, x= -1, scale x = 1
            Vector3 locScale = transform.localScale;
            locScale.x = moveDir.x * -1;
            transform.localScale = locScale;
        }
    }
}
