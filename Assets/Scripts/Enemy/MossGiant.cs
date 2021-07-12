using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
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
