using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // ���� ���� �Ÿ���

    private bool pickupActivated = false;  // ���� ������ �� true
    private bool interactionActivated = false;  // ��ȣ�ۿ� ������ �� true

    [SerializeField] private LayerMask layerMask;  // ������ ���̾�� ����
    [SerializeField] private Inventory inventory;

    [SerializeField] private HandController handController;
    [SerializeField] private GameObject paticle;

    private Collider hitCollider;
    private bool on;

    // ī�޶� �ٶ󺸴� ���⿡ ����
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float raycastRange;
    // ���̾� �浹 ���� �޾ƿ�
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
                inventory.AcquireItem(getItem.item);  // �κ��丮�� �߰�
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
        // �κ��丮�� ������ ������
        pickupActivated = true;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = true;
        // �Һ� ������
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        hitinfo.collider.gameObject.GetComponent<Outline>().enabled = false;
    }
}
