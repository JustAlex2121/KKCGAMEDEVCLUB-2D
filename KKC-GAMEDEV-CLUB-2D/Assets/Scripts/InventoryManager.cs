using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public void StackInInventory(UiSlotHandler currentSlot, ScriptableItem item)
    {
        currentSlot.item.itemCount += item.itemCount;
        currentSlot.itemCountText.text = currentSlot.item.itemCount.ToString();
    }

    public void PlaceInInventory(UiSlotHandler currentSlot, ScriptableItem item)
    {
        currentSlot.item = item;
        currentSlot.icon.sprite = item.itemIcon;
        currentSlot.itemCountText.text = item.itemCount.ToString();
        currentSlot.icon.gameObject.SetActive(true);
    }

    public void ClearItemSlot(UiSlotHandler currentSlot)
    {
        currentSlot.item = null;
        currentSlot.icon.sprite = null;
        currentSlot.itemCountText.text = string.Empty;
        currentSlot.icon.gameObject.SetActive(false);
    }
}