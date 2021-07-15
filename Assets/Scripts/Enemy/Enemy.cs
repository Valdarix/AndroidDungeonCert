using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    private protected bool IsFighting;
    private Transform _playerTransform;
    private int _currentWaypoint;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int InCombat = Animator.StringToHash("InCombat");

    private protected virtual void Init()
    {
        if (waypoints.Count <= 0) return;
        _currentWaypoint = 1;
        _playerTransform = GameObject.FindWithTag("PlayerController").transform;
    }
    
    private void Start()
    {
        Init();
    }
    
    protected virtual void Attack()
    {
        anim.SetBool(InCombat, true);
    }

    public virtual void Update()
    {
        // Determine distance to player, if less than 2f enter combat mode. 
        var distance = Vector2.Distance(transform.position, _playerTransform.position);

        switch (distance)
        { 
            case <= 1.5f when IsFighting:
                return;
            case <= 1.5f:
            {
                
                Attack();
                var direction = _playerTransform.position - transform.position;
                var shouldFlip = direction.x < 0;
                sprite.flipX = shouldFlip;
                IsFighting = true;
                break;
            }
            case > 1.5f:
                anim.SetBool(InCombat, false);
                IsFighting = false;
                break;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Death")) return;
        if (IsFighting) return;
        
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

    
}
