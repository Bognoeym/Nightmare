using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // 습득 가능 거리리

    private bool pickupActivated = false;  // 습득 가능할 시 true
    private bool interactionActivated = false;  // 상호작용 가능할 시 true

    [SerializeField] private LayerMask layerMask;  // 아이템 레이어에만 반응
    [SerializeField] private Inventory inventory;

    [SerializeField] private HandController handController;
    [SerializeField] private GameObject paticle;

    private Collider hitCollider;
    private bool on;

    // 카메라가 바라보는 방향에 적중
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float raycastRange;
    // 레이어 충돌 정보 받아옴
    private RaycastHit hitinfo;
    private GameObject previousItem;

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
            CanInteraction();
        }

        if (Input.GetMouseButtonDown(1))
        {
            handController.GetLighter();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitCollider.transform != null)
            {
                ItemPickUp getItem = hitCollider.GetComponent<ItemPickUp>();
                inventory.AcquireItem(getItem.item);  // 인벤토리에 추가
                //handController.ChangeHand(getItem.item);

                Destroy(hitCollider.gameObject);
                InfoDisappear();
                pickupActivated = false;
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitinfo, raycastRange, layerMask))
        {
            Debug.Log(hitinfo.collider.name);
            previousItem = hitinfo.collider.gameObject;

            switch (hitinfo.collider.gameObject.layer)
            {
                case 6:  // Item
                    ItemInfoAppear();
                    break;
                case 7:  // Interaction
                    InteractionAppear();
                    break;
                default:
                    //InfoDisappear();
                    //InteractionDisappear();
                    break;
            }
        }
        else
        {
            if(previousItem != null)
            {
                pickupActivated = false;
                interactionActivated = false;
                previousItem.GetComponent<Outline>().enabled = false;
                previousItem = null;
            }
        }
    }

    private void CanInteraction()
    {
        if (interactionActivated)
        {
            if (!on)
            {
                on = true;
                paticle.SetActive(true);
            }
        }
    }

    private void InteractionAppear()
    {
        interactionActivated = true;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = true;
    }

    private void InteractionDisappear()
    {
        interactionActivated = false;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = false;
    }

    private void ItemInfoAppear()
    {
        // 인벤토리에 들어오는 아이템
        pickupActivated = true;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = true;
        // 불빛 들어오게
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = false;
    }
}
