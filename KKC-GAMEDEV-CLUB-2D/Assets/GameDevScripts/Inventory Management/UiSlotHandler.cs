using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UiSlotHandler : MonoBehaviour, IPointerClickHandler
{

    public Item item
    public Image icon
    public TextMeshProUGUI itemCountText
    public InventoryManager inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item == null) { return; }
            MouseManager.instance.PickupFromStack(this);
            return;
        }

        MouseManager.instance.UpdateHeldItem(this);
    }

    void Start()
    {
        if (item != null)
        {
            item = item.Clone();
            icon.sprite = item.itemIcon;
            itemCountText = item.itemCount.ToString();
        }
        else
        {
            icon.gameObject.SetActive(false);
            itemCountText.text = string.Empty;
        }
    }

}