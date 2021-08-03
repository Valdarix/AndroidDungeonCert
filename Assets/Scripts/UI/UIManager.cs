using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManger has not been assigned");
            }

            return _instance;
        }
    }

    [SerializeField] private Text playerGemCountText;
    [SerializeField] private Image selectionImage;
    [SerializeField] public Text gemCountHUDText; 
    public void UpdateGemCountText(int gemCount)
    {
        gemCountHUDText.text = gemCount.ToString();
        playerGemCountText.text =  gemCountHUDText.text + " G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition =
            new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    [SerializeField] private GameObject[] healthUnits;
    public void UpdatePlayerHealth(int healthAmount)
    {
        var i = 1;
        foreach (var unit in healthUnits)
        {
            unit.gameObject.SetActive(i <= healthAmount);
            i++;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
