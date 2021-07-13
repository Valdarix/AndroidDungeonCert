using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitTarget = other.GetComponent<IDamagable>();
        hitTarget?.Damage(1);
        
    }
}
