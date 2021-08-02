using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager has not been assigned");
            }

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public bool lvl1CastleKeyPurchased { get; set; }


}
