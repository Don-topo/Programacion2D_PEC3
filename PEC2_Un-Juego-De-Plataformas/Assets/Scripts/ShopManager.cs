using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private readonly int maxBuyItems = 3;
    private ShopItem[] shopItems;

    // Start is called before the first frame update
    void Start()
    {
        PrepareShop();
    }

    private void PrepareShop()
    {

    }

    public void PurchaseItem(GameObject item)
    {
        // TODO Show dialog
        // TODO Can i buy it?       
        // TODO Buy it
        // TODO Destroy Gameobject
        // TODO If is not posible show dialog
    }

    
}
