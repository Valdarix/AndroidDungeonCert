using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
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
        
    }

    public virtual void Update()
    {
        var distance = Vector2.Distance(transform.position, _playerTransform.position);
        
        anim.SetBool(InCombat, !(distance > 2f));

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Death")) return;
        
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
