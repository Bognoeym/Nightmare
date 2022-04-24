using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // 활성화 되어 있으면 다른 활동 제어

    [SerializeField] private GameObject inventoryBase;
    //[SerializeField] private GameObject infoBase;
    [SerializeField] private GameObject slotsParent;  // Grid Setting

    private Slot[] slots;

    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<Slot>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                //Cursor.visible = true;
                OpenInventory();
            }
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        inventoryBase.SetActive(true);
        //infoBase.SetActive(true);
    }

    public void CloseInventory()
    {
        if (inventoryActivated)
            inventoryActivated = !inventoryActivated;
        inventoryBase.SetActive(false);
        //infoBase.SetActive(false);

        //CraftManager.Instance.CloseCraft();
    }

    
    public void AcquireItem(Item item)  // 아이템이 있는지 검사
    {
        //if(일반 열쇠일 경우)
        //{
        //    for (int i = 0; i < slots.Length; i++)
        //    {
        //        if(slots[i].item.itemName == )
        //        {
        //            slots[i].
        //        }
        //    }
        //}

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                slots[i].AddItem(item);
                return;
            }
        }
    }
}