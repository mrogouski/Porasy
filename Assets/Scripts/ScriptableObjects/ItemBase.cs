using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [Header("Item Attributes")]
    public Sprite ItemIcon;
    public string ItemName;
    public ItemType ItemType;
}
