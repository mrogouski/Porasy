using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlotHolder : MonoBehaviour
{
    public GameObject EquippedItem { get; private set; }

    public Transform EquipmentPivot;
    public EquipmentSlotType SlotType;

    /// <summary>
    /// Loads the item prefab into the parent item slot
    /// </summary>
    /// <param name="item">The item to be loaded</param>
    /// <param name="parentTag">The tag used by the parent object</param>
    public void LoadItemPrefab(GameObject item, string parentTag)
    {
        DestroyCurrentItem();

        if (item == null)
        {
            DisableCurrentItem();
            return;
        }

        EquippedItem = Instantiate(item, transform);
        if (EquippedItem != null)
        {
            if (EquipmentPivot != null)
            {
                EquippedItem.transform.parent = EquipmentPivot;
            }
            else
            {
                EquippedItem.transform.parent = transform;
            }

            EquippedItem.transform.localPosition = Vector3.zero;
            EquippedItem.transform.localRotation = Quaternion.identity;
            EquippedItem.transform.localScale = Vector3.one;

            EquippedItem.tag = parentTag;
            TagPrefabChildren(EquippedItem.transform, parentTag);
        }
    }

    private void DisableCurrentItem()
    {
        if (EquippedItem != null)
        {
            EquippedItem.SetActive(false);
        }
    }

    private void DestroyCurrentItem()
    {
        if (EquippedItem != null)
        {
            Destroy(EquippedItem);
        }
    }

    private void TagPrefabChildren(Transform parentTransform, string parentTag)
    {
        if (EquippedItem.transform.childCount > 0)
        {
            foreach (Transform transform in parentTransform)
            {
                transform.tag = parentTag;
                TagPrefabChildren(transform, parentTag);
            }
        }
    }
}
