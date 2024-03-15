using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bu : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] float Speed;

    Sprite spriteDefault;
    [SerializeField]Sprite spriteHit;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;  
        // transform -> x, y, z gizmo라 down은 없음.
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Hit(float _dmg) {
        Hp -= _dmg;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {

            // SpriteRenderer.sprite = spriteHit;
            // Invoke("setSpriteDefault", 0.1f);
        }



    }
    public void setSpriteDefault()
    {

    }
}
