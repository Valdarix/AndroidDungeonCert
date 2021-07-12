using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    
    [SerializeField] private List<Transform> waypoints;
    private int _currentWaypoint;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] protected Animator anim;
    private static readonly int Idle = Animator.StringToHash("Idle");

    private protected virtual void Init()
    {
        if (waypoints.Count <= 0) return;
        _currentWaypoint = 1;
    }
    
    private void Start()
    {
        Init();
    }
    
    public virtual void Attack()
    {
        // Do nothing for now. 
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit")) return;
        
        sprite.flipX = waypoints[_currentWaypoint].transform.gameObject.name switch
        {
            "WaypointA" => true,
            "WaypointB" => false,
            _ => sprite.flipX
        };
        transform.position = Vector2.MoveTowards(transform.position, waypoints[_currentWaypoint].transform.position,
            (speed * Time.deltaTime));

        if (Vector2.Distance(waypoints[_currentWaypoint].transform.position, transform.position) <= 0.1f)
        {
            anim.SetTrigger(Idle);
            waypoints.Reverse();
        }
    }
}
