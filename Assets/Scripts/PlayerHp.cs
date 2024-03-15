using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private GameManager gameManager;
    private Image Hp;
    private Image Effect;
    private Transform trsPlayer;
    [SerializeField] private float curPlayerHp;
    [SerializeField] private float maxPlayerHp;

    private void Awake()
    {
        Hp = transform.Find("Hp").GetComponent<Image>();
        Effect = transform.Find("Effect").GetComponent<Image>();
    }


    void Start()
    {
        gameManager = GameManager.Instance;
        trsPlayer = gameManager.GetPlayerTransform();
    }


    void Update()
    {
        checkPostion();
        checkPlayerHp();
        isDestroying();
    }

    private void isDestroying()
    {
        if (Effect.fillAmount <= 0) { 
            Destroy(gameObject); 
        }
    }

    private void checkPostion()
    {
        if (trsPlayer == null) return;

        Vector3 movePos = trsPlayer.position;//플레이어의 위치
        movePos.y -= 0.7f;
        transform.position = movePos;
    }

    private void checkPlayerHp()
    {

        if (Hp.fillAmount < Effect.fillAmount)
        {
            Effect.fillAmount -= Time.deltaTime * 0.5f;

            if (Effect.fillAmount <= Hp.fillAmount)
            {
                Effect.fillAmount = Hp.fillAmount;
            }
        }
        else if (Hp.fillAmount > Effect.fillAmount)
        {
            Effect.fillAmount = Hp.fillAmount;
        }

        float value = curPlayerHp / maxPlayerHp; // ramined hp ratio
        if (Hp.fillAmount > value)
        {
            Hp.fillAmount -= Time.deltaTime;
            if (Hp.fillAmount <= value)
            {
                Hp.fillAmount = value;
            }
        }
        else if (Hp.fillAmount < value)
        {
            Hp.fillAmount += Time.deltaTime;
            if (Hp.fillAmount >= value)
            {
                Hp.fillAmount = value;
            }
        }
    }

    public void SetPlayerHp(int _hp, int _maxHp) {
        curPlayerHp = _hp;
        maxPlayerHp = _maxHp;
    }
}
