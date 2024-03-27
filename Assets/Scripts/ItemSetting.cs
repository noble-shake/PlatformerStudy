using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSetting : MonoBehaviour
{
    [SerializeField] Sprite sprInven;//�κ��丮���� ���� �̹���

    public void GetItem()
    {
        if (InventoryManager.Instance.GetItem(sprInven))//�������� ������ ����
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("������ â�� ����á��");
        }
    }
}
