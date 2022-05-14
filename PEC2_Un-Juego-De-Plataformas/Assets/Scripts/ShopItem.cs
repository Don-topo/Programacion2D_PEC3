using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class ShopItem : MonoBehaviour
{

    public GameObject interactable;
    public ItemInfo itemInfo;
    public TextMeshPro priceText;


    private void Awake()
    {
        priceText.text = itemInfo.price.ToString();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable.SetActive(false);
        }
    }

    public void Purchase()
    {
        ShopManager.Instance.PurchaseItem(this);
    }

}
