using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject objInventory;
    private List<Transform> listInventory;
    [SerializeField] public GameObject objItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        initInventory();
    }

    private void initInventory()
    {
        Transform[] rangeData = objInventory.transform.GetComponentsInChildren<Transform>(); // Transform 자기 자신은 제거해줘야 함)
        listInventory.AddRange(rangeData);
        listInventory.RemoveAt(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveInventory();
    }

    private void ShowInventory() { 
        
    }

    private void ActiveInventory() {
        if (Input.GetKeyDown(KeyCode.I)) {
            objInventory.SetActive(!objInventory.activeSelf);
        }


    }

    private int getEmptyItemSlot() {
        int count = listInventory.Count;
        for (int iNum = 0; iNum < count; ++iNum) {
            Transform trsSlot = listInventory[iNum];
            if (trsSlot.childCount == 0) {
                return iNum;
            }
        }

        return -1;
    }

    public bool GetItem(Sprite _spr) {
        int slotNum = getEmptyItemSlot();

        if (slotNum == -1) {
            return false;
        }
        Instantiate(objItem);
        return true;
    }

    
}
