using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class ItemInfo 
{
    public LocalizedString itemName;
    public LocalizedString description;
    public int price;
    public LocalizedString buyDialog;
}
