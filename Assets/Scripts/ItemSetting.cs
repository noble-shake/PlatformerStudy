using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSetting : MonoBehaviour
{
    [SerializeField] Sprite sprInven;//인벤토리에서 나올 이미지

    public void GetItem()
    {
        if (InventoryManager.Instance.GetItem(sprInven))//아이템을 넣을수 있음
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("아이템 창이 가득찼음");
        }
    }
}
