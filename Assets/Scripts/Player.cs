using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("벽점프기능")]
    [SerializeField] bool wallStep = false;//벽점프를 할수 있는 조건
    bool isWallStep;//중력조건에서 벽점프를 하게 할지
    [SerializeField] float wallStepTime = 0.3f;//몇초동안 유저가 입력할수 없도록 할것인지
    float wallStepTimer = 0.0f;//타이머

    [Header("대시기능")]
    [SerializeField] float dashTime = 0.3f;
    float dashTimer = 0.0f;//타이머
    [SerializeField] float dashCoolTime = 2.0f;
    float dashCoolTimer = 0.0f;
    TrailRenderer tr;

    [Header("대시스킬 화면 연출")]
    [SerializeField] Image effect;
    [SerializeField] TMP_Text textCool; //dash cool timer 

    [Header("무기 투척")]
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objSword;
    [SerializeField] Transform trsSword;
    [SerializeField] float throwForce;

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
        tr.enabled = false;
    }

    void Start()
    {
    }

    void Update()
    {
        checkGrounded();

        moving();
        doJump();
        doDash();
        checkGravity();

        checkTimers();

        checkUiCooldown();

        checkAim();
    }

    private void checkUiCooldown()
    {
        textCool.gameObject.SetActive(dashCoolTimer != 0.0f);
        textCool.text = (Mathf.CeilToInt(dashCoolTimer)).ToString();

        float amount = 1 - dashCoolTimer / dashCoolTime;
        effect.fillAmount = amount;


    }

    private void checkAim() {
        // ScreenPoint
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector3 newPos = mousePos - transform.position;
        bool isRight = newPos.x > 0 ? true : false;

        if (newPos.x > 0 && transform.localScale.x != -1.0f) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            isRight = true;

        }
        else if (newPos.x < 0 && transform.localScale.x != 1.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            isRight = false;
        }

        Vector3 direction = isRight == true ? Vector3.right : Vector3.left;

        // y축 12시 기준으로 0도부터, 설정은 마음대로.
        ///float angle = Quaternion.FromToRotation(Vector3.up, mousePos - transform.position).eulerAngles.z;
        float angle = Quaternion.FromToRotation(direction, newPos).eulerAngles.z;
        Debug.Log(360 - angle);
        angle = isRight == true ? -angle : angle;


        // World / ViewPort / ScreenPoint
        // World 기준으로 캐릭터로부터 마우스가 몇도인지를 알아내도록 한다.
        // Camera.main.ScreenToWorldPoint(mousePos)


        trsHand.localRotation = Quaternion.Euler(0, 0, angle);


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
        if (wallStepTimer != 0.0f || dashTimer != 0.0f) return;//만약 타이머가 구동중이면 이동을 입력받을수 없음

        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//-1,0,1
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
        //0 false 1,-1 true
        anim.SetBool("Walk", moveDir.x != 0.0f);

        //if (moveDir.x != 0.0f) //오른쪽으로 갈때 x값은1 스케일x는 -1, 왼쪽으로 갈때 x 는 -1 스케일 x 1
        //{
        //    Vector3 locScale = transform.localScale;
        //    locScale.x = Input.GetAxisRaw("Horizontal") * -1;
        //    transform.localScale = locScale;
        //}
    }

    private void doJump()//유저가 스페이스키를 누른다면 점프할수있게 준비
    {
        if (isGround == false) //공중에 떠있을때
        {
            if (Input.GetKeyDown(KeyCode.Space) && wallStep == true && moveDir.x != 0)
            {
                isWallStep = true;
            }
        }
        else//바닥에 있을때
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }
        }
    }

    private void doDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer == 0.0f && dashCoolTimer == 0.0f)
        {
            verticalVelocity = 0.0f;

            bool dirRight = transform.localScale.x == -1;//오른쪽을 보고있는지
            rigid.velocity = new Vector2(dirRight == true ? 20.0f : -20.0f, verticalVelocity);

            dashTimer = dashTime;
            dashCoolTimer = dashCoolTime;

            tr.enabled = true;
        }
    }

    private void shootWeapon() {
        if (Input.GetKeyDown(0)) {
            GameObject go = Instantiate(objSword, trsSword.position, trsSword.rotation);
            ThrowWeapon gosc = go.GetComponent<ThrowWeapon>();
            // if(gosc != null)
        }
    }

    private void checkGravity()
    {
        if (dashTimer != 0.0f) return;

        if (isWallStep == true)
        {
            isWallStep = false;

            Vector2 dir = rigid.velocity;
            dir.x *= -1;
            rigid.velocity = dir;//현재 보고있는 방향의 반대
            verticalVelocity = jumpForce;//점프력

            wallStepTimer = wallStepTime;//벽점프 입력불가 대기시간을 타이머에 입력
        }
        else if (isGround == false)//공중에 있을때
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

    public void TriggerEnter(HitBox.enumHitType _hitType, Collider2D _coll)
    {
        switch (_hitType)
        {
            case HitBox.enumHitType.WallCheck:
                wallStep = true;
                break;
            case HitBox.enumHitType.ItemCheck:

                break;
        }
    }

    public void TriggerExit(HitBox.enumHitType _hitType, Collider2D _coll)
    {
        switch (_hitType)
        {
            case HitBox.enumHitType.WallCheck:
                wallStep = false;
                break;
        }
    }

    private void checkTimers()
    {
        if (wallStepTimer > 0.0f)
        {
            wallStepTimer -= Time.deltaTime;
            if (wallStepTimer < 0.0f)
            {
                wallStepTimer = 0.0f;
            }
        }

        if (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer < 0.0f)
            {
                dashTimer = 0.0f;
                tr.enabled = false;
                tr.Clear();
            }
        }

        if (dashCoolTimer > 0.0f)
        {
            dashCoolTimer -= Time.deltaTime;
            if (dashCoolTimer < 0.0f)
            {
                dashCoolTimer = 0.0f;
            }
        }
    }
}

