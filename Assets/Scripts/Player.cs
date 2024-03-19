using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Vector3 moveDir;
    [SerializeField] BoxCollider2D boxColl;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float verticalVelocity;

    [SerializeField] bool isJump;
    [SerializeField] bool isGround;

    [SerializeField] float rayDistance = 1f;
    [SerializeField] Color rayColor;
    [SerializeField] bool showRay = false;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (showRay == true) {
            Gizmos.color = rayColor;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayDistance));

            // Gizmos color = Color.blue;
            // Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayDistance));
        }

    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();
        moving();
        doJump();
        checkGravity();
    }

    private void doJump()
    {
        if (isGround == false) return;

        if (Input.GetKeyDown(KeyCode.Space)) {
            isJump = true;
        }
    }

    private void checkGravity()
    {
        if (isGround == false) {
            verticalVelocity = Physics2D.gravity.y * Time.deltaTime;
            if (verticalVelocity < -10.0f)
            {
                verticalVelocity = -10.0f;
            }
            else {
                if (isJump == true) {
                    isJump = false;
                    verticalVelocity = jumpForce;
                }
                if (verticalVelocity < 0) {
                    verticalVelocity = 0f;
                }
            }
        }
        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        // RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask(Tool.GetLayer(Layers.Ground))); //this int exactly different general int value. maybe checksome Layer gab.
        RaycastHit2D ray = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector3.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));  //bounds -> bounding box concept

        if (ray) {
            isGround = true;    
        }
    }

    private void moving() {
       moveDir.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
        anim.SetBool("Walk", moveDir.x != 0.0f);

        if (moveDir.x != 0.0f) { //go to right then x=1, scale x = -1, if left, x= -1, scale x = 1
            Vector3 locScale = transform.localScale;
            locScale.x = -1 * Input.GetAxis("Horizontal");
            transform.localScale = locScale;
        }
    }
}
