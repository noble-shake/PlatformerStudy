using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager_bu : MonoBehaviour
{
    // 2024-02-02 ~ GroundManager Implements
    // Access to Shader and Control Offset for Map Scrolling.
    // Check the Sprites Wrap Mode (clamp/repeat)
    // Check Material Shader Info (Properties Name may different.)
    // use Material's offset for bacground scroll.

    // 2024-02-07

    // you can use SerializedField, but can be implemented in script and prevent to designer's mistake.
    // Material is one of SpriteRenderer function
    private Material matBottom;
    private Material matMiddle;
    private Material matTop; // CLASS => call by ref

    [SerializeField] private float speedBottom;
    [SerializeField] private float speedMiddle;
    [SerializeField] private float speedTop;

    // private SpriteRenderer sprRenderer;


    private void init()
    {
        // if you want find GameObject.  (1), (2) conditionally slow. (3) already know itself
        // (1). GameObject.Find will search at Root Object to Last Object. it is very unefficient and slow. NOT RECOMMENDED but indispensable.
        // GameObject objBottom = GameObject.Find("GroundManager/SprBottom");
        // (2). use strategy for SprBottom Object have both transform and SpriteRenderer;
        // Transform trsBottom = Transform.FindObjectOfType<SpriteRenderer>().transform;
        // Transform[] trsBottom = Transform.FindObjectsOfType<SpriteRenderer>();

        // (3). "gameObject" or "transform". this variables are default variable from Object owned this script
        // From Parent Object To Child Object
        // transform.Find("Root").transform.Find("Parent").transform.Find("Child")  ===> transform.Find("Root/Parent/Child")
        //transform.Find("SprBottom/SprMiddle").gameObject.transform;
        // transform.root
        // transform.parent
        // transform.childCount(0);

        // transform.Find("SprBottom"); // 자식의 이름을 알고 0있을 때!

        // (4) if you easily know. this object's childs indexes.
        // transform.GetChild(0);


        // 2024-02-07
        SpriteRenderer SRTop = transform.Find("SprTop").GetComponent<SpriteRenderer>(); // SpriteRenderer
        matTop = SRTop.material;

        SpriteRenderer SRMiddle = transform.Find("SprMiddle").GetComponent<SpriteRenderer>(); // SpriteRenderer
        matMiddle = SRMiddle.material;

        SpriteRenderer SRBottom = transform.Find("SprBottom").GetComponent<SpriteRenderer>(); // SpriteRenderer
        matBottom = SRBottom.material;

        //SpriteRenderer dummy = transform.GetComponentChildren<SpriteRenderer>(true);  // 자기 자신도 포함.
        //
     }

    void Start()
    {
        init();
    }


    void Update()
    {
        //2024-02-07
        // [PREVIOUS] Time.deltaTime. Frame 처리는 컴퓨터간, 오브젝트간 달라질 수있기 때문에.. 이를 초로 맞춰주는 Time.deltatime이 중요.
        // [LEARN] Material's offset will change background rolling.

        // matBottom.offset ??????????
        // *shader code and properties will be different. we have to check properties.
        // mainTextureOffset is struct (CBV)  ~> as method,  구조 자체로 값을 넣어줘야 변경 가능함. x,y 값을 임의로 못 바꾼다.

        /* https://docs.unity3d.com/ScriptReference/Material-mainTextureOffset.html
        * Description
        * The offset of the main texture.
        * 
        * By default, Unity considers a texture with the property name name "_MainTex" to be the main texture. Use the [MainTexture] ShaderLab Properties attribute to make Unity consider a texture with a different property name to be the main texture.
        * 
        * This is the same as calling Material.GetTextureOffset or Material.SetTextureOffset with the property name of the main texture as a parameter.
        * 
        * Additional resources: SetTextureOffset, GetTextureOffset, ShaderLab: Properties, ShaderPropertyFlags.MainTexture.
        */

        // Vector2, Vector3, Vector4 ....
        //Vector2 vecBottom = Vector2.zero;
        //vecBottom.x = 1.2f;


        Vector2 vecBottom = matBottom.mainTextureOffset;        
        Vector2 vecMiddle = matMiddle.mainTextureOffset;        
        Vector2 vecTop = matTop.mainTextureOffset;

        vecBottom += new Vector2(0, speedBottom * Time.deltaTime);
        vecMiddle += new Vector2(0, speedMiddle * Time.deltaTime);
        vecTop += new Vector2(0, speedTop* Time.deltaTime);


        //mathf -> C#
        //Mathf -> Unity
        vecBottom.y = Mathf.Repeat(vecBottom.y, 1.0f);
        vecMiddle.y = Mathf.Repeat(vecMiddle.y, 1.0f);
        vecTop.y = Mathf.Repeat(vecTop.y, 1.0f);



        matBottom.mainTextureOffset = vecBottom;
        matMiddle.mainTextureOffset = vecMiddle;
        matTop.mainTextureOffset = vecTop;

    }
}
