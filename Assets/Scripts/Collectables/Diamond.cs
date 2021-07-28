using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    // Start is called before the first frame update
    private int _gemValue = 1;
    private bool collected;

    public void SetValue(int value)
    {
        _gemValue = value;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("PlayerController")) return;
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        player.AddGems(_gemValue);
        collected = true;
        Destroy(gameObject);
    }
}
