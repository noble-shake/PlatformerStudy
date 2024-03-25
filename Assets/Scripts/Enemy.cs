using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigid;
    Collider2D groundCheckColl;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        groundCheckColl = transform.GetChild(0).GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = rigid.velocity * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground")) == false) {
            flip();
        }
    }

    private void flip()
    {
        speed *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
