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
    [Header("�÷��̾� �̵��� ����")]
    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxColl;

    [SerializeField] float moveSpeed = 5f;//�̵��ӵ�
    [SerializeField] float jumpForce = 5f;//�����ϴ� ��
    [SerializeField] bool isGround;

    bool isJump;
    float verticalVelocity = 0f;

    [SerializeField] float rayDistance = 1f;
    [SerializeField] Color rayColor;
    [SerializeField] bool showRay = false;

    Vector3 moveDir;

    [Header("���������")]
    [SerializeField] bool wallStep = false;//�������� �Ҽ� �ִ� ����
    bool isWallStep;//�߷����ǿ��� �������� �ϰ� ����
    [SerializeField] float wallStepTime = 0.3f;//���ʵ��� ������ �Է��Ҽ� ������ �Ұ�����
    float wallStepTimer = 0.0f;//Ÿ�̸�

    [Header("��ñ��")]
    [SerializeField] float dashTime = 0.3f;
    float dashTimer = 0.0f;//Ÿ�̸�
    [SerializeField] float dashCoolTime = 2.0f;
    float dashCoolTimer = 0.0f;
    TrailRenderer tr;

    [Header("��ý�ų ȭ�� ����")]
    [SerializeField] Image effect;
    [SerializeField] TMP_Text textCool; //dash cool timer 

    [Header("���� ��ô")]
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objSword;
    [SerializeField] Transform trsSword;
    [SerializeField] float throwForce;
    bool isRight;

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
        shootWeapon();
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
        isRight = newPos.x > 0 ? true : false;

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

        // y�� 12�� �������� 0������, ������ �������.
        ///float angle = Quaternion.FromToRotation(Vector3.up, mousePos - transform.position).eulerAngles.z;
        float angle = Quaternion.FromToRotation(direction, newPos).eulerAngles.z;
        Debug.Log(360 - angle);
        angle = isRight == true ? -angle : angle;


        // World / ViewPort / ScreenPoint
        // World �������� ĳ���ͷκ��� ���콺�� ������� �˾Ƴ����� �Ѵ�.
        // Camera.main.ScreenToWorldPoint(mousePos)


        trsHand.localRotation = Quaternion.Euler(0, 0, angle);


    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f) return;

        //RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));        
        RaycastHit2D ray = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, rayDistance, LayerMask.GetMask(Tool.GetLayer(Layers.Ground)));
        if (ray)//Ground�� ����
        {
            isGround = true;
        }
    }

    private void moving()
    {
        if (wallStepTimer != 0.0f || dashTimer != 0.0f) return;//���� Ÿ�̸Ӱ� �������̸� �̵��� �Է¹����� ����

        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//-1,0,1
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
        //0 false 1,-1 true
        anim.SetBool("Walk", moveDir.x != 0.0f);

        //if (moveDir.x != 0.0f) //���������� ���� x����1 ������x�� -1, �������� ���� x �� -1 ������ x 1
        //{
        //    Vector3 locScale = transform.localScale;
        //    locScale.x = Input.GetAxisRaw("Horizontal") * -1;
        //    transform.localScale = locScale;
        //}
    }

    private void doJump()//������ �����̽�Ű�� �����ٸ� �����Ҽ��ְ� �غ�
    {
        if (isGround == false) //���߿� ��������
        {
            if (Input.GetKeyDown(KeyCode.Space) && wallStep == true && moveDir.x != 0)
            {
                isWallStep = true;
            }
        }
        else//�ٴڿ� ������
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

            bool dirRight = transform.localScale.x == -1;//�������� �����ִ���
            rigid.velocity = new Vector2(dirRight == true ? 20.0f : -20.0f, verticalVelocity);

            dashTimer = dashTime;
            dashCoolTimer = dashCoolTime;

            tr.enabled = true;
        }
    }

    private void shootWeapon() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject go = Instantiate(objSword, trsSword.position, trsSword.rotation);
            ThrowWeapon gosc = go.GetComponent<ThrowWeapon>();
            // transform -> rotation ���
            // vector -> world position
            Vector2 throwForce = isRight == true ? new Vector2(10f, 0f) : new Vector2(0f, 10f);
            gosc.SetForce(trsSword.transform.rotation * throwForce, isRight);
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
            rigid.velocity = dir;//���� �����ִ� ������ �ݴ�
            verticalVelocity = jumpForce;//������

            wallStepTimer = wallStepTime;//������ �ԷºҰ� ���ð��� Ÿ�̸ӿ� �Է�
        }
        else if (isGround == false)//���߿� ������
        {
            verticalVelocity -= 9.81f * Time.deltaTime;

            if (verticalVelocity < -10.0f)
            {
                verticalVelocity = -10.0f;
            }
        }
        else//���� �پ�������
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

