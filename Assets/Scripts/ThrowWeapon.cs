using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D coll;
    Vector2 force;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation *= Quaternion.Euler(0, 0, -1); // multiply equalt to plus/subtract in quaternion.
        transform.Rotate(new Vector3(0f, 0f, isRight == true ? -360f : 360f));
    }

    public void SetForce(Vector2 _force, bool _isRight) {
        force = _force;
        isRight = _isRight;
    }
}
