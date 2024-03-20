using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 이동과 점프")]
    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxColl;

    [SerializeField] float moveSpeed = 5f;//이동속도
    [SerializeField] float jumpForce = 5f;//점프하는 힘
    [SerializeField] bool isGround;

    bool isJump;
    float verticalVelocity = 0f;

    [SerializeField] float rayDistance = 1f;
    [SerializeField] Color rayColor;
    [SerializeField] bool showRay = false;

    Vector3 moveDir;

    [Header("wall jump")]
    [SerializeField] bool wallStep = false;
    bool isWallStep; //in gravity jumparable
    [SerializeField] float wallStepTimer = 0.3f;
    float wallStepTime = 0.0f;

    [Header("대시")]
    [SerializeField] float dashTime = 0.3f;
    float dashTimer = 0.0f;
    [SerializeField] float dashCoolTime = 2.0f;
    float dashCoolTimer = 0.0f;
    TrailRenderer tr;
    // tr.enabled = false;



    private void OnDrawGizmos()
    {
        if (showRay == true)
        {
            Gizmos.color = rayColor;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayDistance));
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        tr = GetComponent<TrailRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {
        checkGrounded();

        moving();
        doDash();
        doJump();
        checkGravity();

        checkTimers();
    }

    private void doDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer == 0.0f && dashCoolTimer == 0.0f) {
            
            verticalVelocity = 0.0f;

            bool dirRight = (transform.localScale.x == -1);
            rigid.velocity = new Vector2(dirRight == true ? 20.0f : -20.0f, verticalVelocity);
            dashTimer = dashTime;
            dashCoolTimer = dashCoolTime;
        }
    }

    private void checkTimers()
    {
        if (wallStepTimer > 0.0f) {
            wallStepTimer -= Time.deltaTime;
            if (wallStepTimer < 0.0f) {
                wallStepTimer = 0.0f;
            }
        }

        if (dashTimer > 0.0f) {
            dashTimer -= Time.deltaTime;
            if (dashTimer < 0.0f) {
                dashTimer = 0.0f;
            }
        }

        if (dashCoolTimer > 0.0f) {
            dashCoolTime -= Time.deltaTime;
            if (dashCoolTimer < 0.0f) {
                dashCoolTimer = 0.0f;
                tr.enabled = false;
                tr.Clear();
            }
        }
    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        //RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));        
        RaycastHit2D ray = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        if (ray)//Ground에 닿음
        {
            isGround = true;
        }
    }

    private void moving()
    {
        if (wallStepTimer != 0.0f || dashTimer != 0.0f) return;

        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//-1,0,1
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
        //0 false 1,-1 true
        anim.SetBool("Walk", moveDir.x != 0.0f);

        if (moveDir.x != 0.0f) //오른쪽으로 갈때 x값은1 스케일x는 -1, 왼쪽으로 갈때 x 는 -1 스케일 x 1
        {
            Vector3 locScale = transform.localScale;
            locScale.x = Input.GetAxisRaw("Horizontal") * -1;
            transform.localScale = locScale;
        }
    }

    private void doJump()//유저가 스페이스키를 누른다면 점프할수있게 준비
    {
        if (isGround == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && wallStep == true && moveDir.x != 0)
            {
                isWallStep = true;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }
        }


    }

    private void checkGravity()
    {
        // if (isDash == true) return;
        if (dashTimer != 0.0f) return;

        if (isWallStep == true) {
            isWallStep = false;
            Vector2 dir = rigid.velocity;
            dir.x *= -1;
            rigid.velocity= dir;
            verticalVelocity = jumpForce;

            wallStepTimer = wallStepTime; //벽점프 입력불가 대기시간을 타이머에 입력
        }

        if (isGround == false)//공중에 있을때
        {
            verticalVelocity -= 9.81f * Time.deltaTime;

            if (verticalVelocity < -10.0f)
            {
                verticalVelocity = -10.0f;
            }
        }
        else//땅에 붙어있을때
        {
            if (isJump == true)
            {
                isJump = false;
                verticalVelocity = jumpForce;
            }
            else if (verticalVelocity < 0)
            {
                verticalVelocity = 0f;
            }
        }

        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

    public void TriggerEnter(HitBox.enumHitType _hitType, Collider2D other)
    {
        switch (_hitType) {
            case HitBox.enumHitType.WallCheck:
                wallStep = true;
                break;
            case HitBox.enumHitType.ItemCheck:

                break;
        }
    }

    public void TriggerExit(HitBox.enumHitType _hitType, Collider2D other)
    {
        switch (_hitType)
        {
            case HitBox.enumHitType.WallCheck:

                break;
            case HitBox.enumHitType.ItemCheck:

                break;
        }
}
}
