using System.Collections;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    public int Health { get; set; }
    private bool _canBeAttacked;
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    

    private protected override void Init()
    {
        base.Init();
        Health = health;
        _canBeAttacked = true;
    }
    public void Damage(int damageAmount)
    {
        if (!_canBeAttacked) return;
        anim.SetTrigger(Hit);
        Health -= damageAmount;
      
        if (Health < 1)
        {
            anim.SetBool("InCombat", false);
            IsFighting = false;
            anim.SetTrigger(Death);
            Destroy(transform.parent.gameObject, 1.5f);
        }
        _canBeAttacked = false;
    }

    protected override void Attack()
    {
        if (IsFighting) return;
        base.Attack();
        StartCoroutine(Combat());
    }

    private IEnumerator Combat()
    {
        while (Health > 0)
        {
            anim.SetTrigger(AttackTrigger);
            yield return new WaitForSeconds(0.1f);
            anim.ResetTrigger(AttackTrigger); 
            yield return new WaitForSeconds(1.25f); 
            if (!IsFighting)
                yield break;
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
