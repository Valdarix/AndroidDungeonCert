using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    public int Health { get; set; }
    private protected override void Init()
    {
        base.Init();
        Health = health;
    }
    public void Damage(int damageAmount)
    {
        anim.SetTrigger(Hit);
        Health -= damageAmount;
        
        if (Health < 1)
        {
            Destroy(gameObject);
        }

    }
}
