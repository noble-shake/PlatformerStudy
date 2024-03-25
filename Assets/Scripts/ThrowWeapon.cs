using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 force;
    bool isRight;

    [SerializeField] float isTriggerTime = 1.0f;
    float timer;
    bool doTrigger = false;
    [SerializeField] Collider2D coll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (doTrigger == false) {
            doTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation *= Quaternion.Euler(0, 0, -1); // multiply equalt to plus/subtract in quaternion.
        transform.Rotate(new Vector3(0f, 0f, (isRight == true ? -360f : 360f) * Time.deltaTime));

        if (doTrigger == true) {
            timer += Time.deltaTime;
            if (timer > isTriggerTime) {
                coll.isTrigger = true;
            }
        }

    }

    public void SetForce(Vector2 _force, bool _isRight) {
        force = _force;
        isRight = _isRight;
    }
}
