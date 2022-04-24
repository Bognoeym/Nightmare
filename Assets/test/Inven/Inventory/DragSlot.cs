using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;

    // 아이템 이미지
    [SerializeField]
    private Image itemImage;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image image)
    {
        itemImage.sprite = image.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
