using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    [SerializeField] private List<Transform> waypoints;
    private int _currentWaypoint;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        if (waypoints.Count <= 0) return;
        _currentWaypoint = 1;
    }
    public override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MossGiant_Idle_Anim")) return;
        
        sprite.flipX = waypoints[_currentWaypoint].transform.gameObject.name switch
        {
            "WaypointA" => true,
            "WaypointB" => false,
            _ => sprite.flipX
        };
        transform.position = Vector2.MoveTowards(transform.position, waypoints[_currentWaypoint].transform.position,
            (speed * Time.deltaTime));

        if (Vector2.Distance(waypoints[_currentWaypoint].transform.position, transform.position) <= 0.3f)
        {
            _currentWaypoint++;
            anim.SetTrigger(Idle);
        }

        if (_currentWaypoint != waypoints.Count) return;
        waypoints.Reverse();
        _currentWaypoint = 0;
    }

}
