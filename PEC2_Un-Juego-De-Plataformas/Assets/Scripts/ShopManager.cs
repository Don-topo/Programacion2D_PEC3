using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private static ShopManager shopManager;
    public static ShopManager Instance { get { return shopManager; } }
    public ParticleSystem purchaseparticles;

    // Start is called before the first frame update
    private void Awake()
    {
        if(shopManager != null && shopManager != this)
        {
            Destroy(this);
        }
        else
        {
            shopManager = this;
        }
    }

    public void PurchaseItem(ShopItem item)
    {
        // TODO Show dialog    
        ItemInfo itemInfo = item.GetComponent<ShopItem>().itemInfo;
        if (GameManager.Instance.CanPurchaseItem(itemInfo.price))
        {
            GameManager.Instance.PurchaseItem(itemInfo.price);
            Destroy(item.gameObject);
            purchaseparticles.transform.position = item.transform.position;
            purchaseparticles.Play();
            ItemAction(item.name);
            // TODO If is posible show dialog
        }
        else
        {
            // TODO can't buy dialog
            
        }



    }

    private void ItemAction(string name)
    {
        switch (name)
        {
            case "SwordBuy":
                GameManager.Instance.IncreasePlayerDamage(1);
                break;
            case "HealthBuy":
                GameManager.Instance.IncreaseLife();
                break;
            case "PotionBuy":
                GameManager.Instance.PickUpPotion();
                break;
        }
    }

    
}
