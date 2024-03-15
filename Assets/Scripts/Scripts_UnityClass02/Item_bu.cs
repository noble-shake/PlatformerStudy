using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//열거형 자료형
// both have int type and string
//public enum eItemType { 
//    Recovery, // 0
//    PowerUp // 1
//}


public class Item_bu : MonoBehaviour
{
    [SerializeField] eItemType itemType;
    private Vector3 moveDir;
    private float speed;

    // casting
    int value = (int)eItemType.PowerUp;
    string value2 = eItemType.PowerUp.ToString();

    eItemType type = (eItemType)1;
    //eItemType type2 = System.Enum.Parse(typeof(eItemType), "PowerUp");
    // Start is called before the first frame update
    void Start()
    {


        float dirX = Random.Range(-1.0f, 1.0f);
        float dirY = Random.Range(-1.0f, 1.0f);

        moveDir = new Vector2(dirX, dirY);

        //speed = Random.Range(speedMinMax.x, speedMinMax.y);
    }

    // Update is called once per frame
    void Update()
    {


        transform.position += moveDir * speed * Time.deltaTime;
    }
}
