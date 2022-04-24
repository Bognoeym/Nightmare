using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Transform tf_ItemPos;  // 아이템 위치할 곳
    public static GameObject go_HandItem;  // 손에 든 아이템

    [SerializeField] private GameObject go_lighter;
    private bool Get;

    public void ChangeHand(Item item = null)
    {
        if (item != null)
        {
            go_HandItem = Instantiate(item.itemPrefab, tf_ItemPos.position, tf_ItemPos.rotation);
            go_HandItem.GetComponent<Rigidbody>().isKinematic = true;
            go_HandItem.GetComponent<BoxCollider>().enabled = false;
            // go_HandItem.layer = 3;  // Hand
            go_HandItem.transform.SetParent(tf_ItemPos);
        }
    }

    public void GetLighter()
    {
        if (Get)
            Get = false;
        else
            Get = true;

        go_lighter.SetActive(Get);
    }
}
