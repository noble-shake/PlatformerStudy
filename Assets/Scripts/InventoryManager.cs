using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject objInventory;
    List<Transform> listInventory = new List<Transform>();
    [SerializeField] GameObject objItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        initInventory();
    }

    private void initInventory()
    {
        Transform[] rangeData = objInventory.transform.GetComponentsInChildren<Transform>();
        listInventory.AddRange(rangeData);
        listInventory.RemoveAt(0);
    }

    void Update()
    {
        AcitveInventory();
    }

    private void AcitveInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            objInventory.SetActive(!objInventory.activeSelf);
        }
    }

    /// <summary>
    /// ����ִ� ������ ���� ��ȣ�� �����մϴ�.
    /// </summary>
    /// <returns></returns>
    private int getEmptyItemSlot()
    {
        int count = listInventory.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            Transform trsSlot = listInventory[iNum];
            if (trsSlot.childCount == 0)
            {
                return iNum;
            }
        }
        return -1;
    }

    public bool GetItem(Sprite _spr)
    {
        int slotNum = getEmptyItemSlot();
        if (slotNum == -1)
        { 
            return false;    //������ ���� ����
        }
        //Todo
        GameObject go = Instantiate(objItem, listInventory[slotNum]);
        DragableUi ui = go.GetComponent<DragableUi>();
        ui.SetItem(_spr);

        return true;//������ ���� ����
    }

    public bool isActiveInventory()
    {
        return objInventory.activeSelf;
    }
}
