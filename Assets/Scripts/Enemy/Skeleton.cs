using System.Collections;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    public int Health { get; set; }
    private bool _canBeAttacked;
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int InCombat = Animator.StringToHash("InCombat");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
 
    private protected override void Init()
    {
        base.Init();
        Health = health;
        _canBeAttacked = true;
    }
    public void Damage(int damageAmount)
    {
        StartCoroutine(Combat());
        if (!_canBeAttacked) return;
        anim.SetTrigger(Hit);
        Health -= damageAmount;
        
        if (Health < 1)
        {
            anim.SetTrigger(Death);
            Destroy(gameObject, 1f);
        }
        _canBeAttacked = false;
    }

    private IEnumerator Combat()
    {
        anim.SetBool(InCombat, true);
        while (Health > 0)
        {
            anim.SetTrigger(AttackTrigger);
            yield return new WaitForSeconds(0.25f);
            anim.ResetTrigger(AttackTrigger); 
            yield return new WaitForSeconds(1.25f); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _canBeAttacked = true;
    }
}
