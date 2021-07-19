using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected int speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    
    private bool _isFighting;
    private bool _canBeAttacked;
    private Transform _playerTransform;
    private int _currentWaypoint;
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int InCombat = Animator.StringToHash("InCombat");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    public int Health { get; set; }

    private protected virtual void Init()
    {
        if (waypoints.Count <= 0) return;
        _currentWaypoint = 1;
        _playerTransform = GameObject.FindWithTag("PlayerController").transform;
        Health = health; 
        _canBeAttacked = true;
    }
    
    private void Start()
    {
        Init();
    }
    
    public virtual void Attack()
    {
        if (!CanAttackFromAnimation()) return;
        anim.SetBool(InCombat, true);
        if (_isFighting) return;
        _isFighting = true;
        StartCoroutine(Combat());
    }

    public virtual void Update()
    {
        // Determine distance to player, if less than 2f enter combat mode. 
        if (_playerTransform != null)
        {
            var distance = Vector2.Distance(transform.position, _playerTransform.position);

            switch (distance)
            {
                case <= 1.5f when _isFighting:
                    return;
                case <= 1.5f:
                {
                    if (health > 0)
                    {
                        Attack();
                        var direction = _playerTransform.position - transform.position;
                        var shouldFlip = direction.x < 0;
                        sprite.flipX = shouldFlip;
                    }
                    break;
                }
                case > 1.5f:
                    anim.SetBool(InCombat, false);
                    _isFighting = false;
                    break;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Death")) return;
        if (_isFighting) return;
        
        sprite.flipX = waypoints[_currentWaypoint].transform.gameObject.name switch
        {
            "WaypointA" => true,
            "WaypointB" => false,
            _ => sprite.flipX
        };
        transform.position = Vector2.MoveTowards(transform.position, waypoints[_currentWaypoint].transform.position,
            (speed * Time.deltaTime));

        if (!(Vector2.Distance(waypoints[_currentWaypoint].transform.position, transform.position) <= 0.1f)) return;
        anim.SetTrigger(Idle);
        waypoints.Reverse();
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
    
    public void Damage(int damageAmount)
    {
        if (!_canBeAttacked) return;
        anim.SetTrigger(Hit);
        Health -= damageAmount;
        if (Health < 1)
        {
            anim.SetBool(InCombat, false);
            _isFighting = false;
            anim.SetBool(Death, true);
            Destroy(transform.parent.gameObject, 1.5f);
        }
        _canBeAttacked = false;
    }
    
    private IEnumerator Combat()
    {
        while (Health > 0)
        {
            if (!_isFighting)
            {
                yield break;
            }
            anim.SetTrigger(AttackTrigger);
            yield return new WaitForSeconds(0.1f);
            anim.ResetTrigger(AttackTrigger); 
            yield return new WaitForSeconds(1.50f);
        }
    }
    
    private bool CanAttackFromAnimation()
    {
        var stateInfo = !anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Death");

        return stateInfo;
    }

}
