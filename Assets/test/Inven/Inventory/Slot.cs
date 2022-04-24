using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour //, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public Image itemImage;

    // ������ �̹����� ���� ����
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void AddItem(Item collect)
    {
        item = collect;
        //itemImage.sprite = item.itemImage;
        //SetColor(1);
    }

    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        //SetColor(0);
    }

    /*
    // �� ��ũ��Ʈ�� ���� ������Ʈ�� ���콺�� Ŭ���ϸ� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // ������ ���콺 Ŭ��
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && item.itemType == Item.ItemType.Used)
            {
                if (!CraftManager.manufacActivated)
                {
                    // ������ �Ҹ�
                    itemEffectDatabase.UseItem(item);
                    SetSlotCount(-1);
                }
            }
        }

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                ItemInfoUI.Instance.SetInfo(item);
            }
        }
    }

    // 
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
        print("OnBeginDrag");
    }

    // �巡�� �� ����
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
        print("OnDrag");
    }

    // �巡�װ� �����⸸ �ϸ� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        //if (DragSlot.instance.transform.localPosition.x < baseRect.xMin ||
        //    DragSlot.instance.transform.localPosition.x > baseRect.xMax ||
        //    DragSlot.instance.transform.localPosition.y < baseRect.yMin ||
        //    DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        //{
        //    //Instantiate(DragSlot.instance.dragSlot.item.itemPrefab);
        //    DragSlot.instance.dragSlot.ClearSlot();
        //    SetSlotCount(-1);
        //    print("??");
        //}

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        print("OnEndDrop");
    }

    // �ڱ� �ڽ��� �ƴ� ������ �巡�׸� ������ �� ����
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
        print("OnDrop");
    }

    private void ChangeSlot()
    {
        Item tmpItem = item;
        int tmpItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(tmpItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(tmpItem, tmpItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
    */
}
