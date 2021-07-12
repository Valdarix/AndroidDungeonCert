using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }

    private protected override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage(int damageAmount)
    {
        
    }
}
