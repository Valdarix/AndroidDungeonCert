using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Merchant_Shop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject shopPanel;
    private int currentItemSelected;
    private Player player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerController")) return;
        player = other.GetComponent<Player>();

        if (player != null)
        {
            UIManager.Instance.UpdateGemCountText(player.GetCurrentGems());
            shopPanel.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerController")) return;
        shopPanel.SetActive(false);
    }

    public void SelectItem(int itemSelected)
    {
        var yPos = itemSelected switch
        {
            0 => 223,
            1 => 92,
            2 => -40,
            _ => 0
        };
        currentItemSelected = itemSelected;
        UIManager.Instance.UpdateShopSelection(yPos);
    }

    public void BuySelectedItem()
    {
        var cost = currentItemSelected switch
        {
            0 => 200,
            1 => 400,
            2 => 100,
            _ => 0
        };
        
        if (player.GetCurrentGems() ! >= cost) return;
        
        player.RemoveGems(cost);
        UIManager.Instance.UpdateGemCountText(player.GetCurrentGems());
    }
    
}
