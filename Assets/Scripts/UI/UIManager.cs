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
    
    public void UpdateGemCountText(int gemCount)
    {
        playerGemCountText.text = gemCount + " G";
    }

    public void UpdateShopSelection(int yPos)
    {
        
        selectionImage.rectTransform.anchoredPosition =
            new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }


    private void Awake()
    {
        _instance = this;
    }
}
