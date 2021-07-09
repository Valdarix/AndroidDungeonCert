using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private Player_Animation_Contoller animationContoller;
   

    private Rigidbody2D _playerRigidbody;

    private bool _attacking;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }

    }
    private void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector2( 0, jumpPower);
    }
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position ,Vector2.down, 1f);
        Debug.DrawRay(transform.position,Vector3.down * 1f,Color.green);
        var checkResult =  groundCheck.collider != null && groundCheck.collider.CompareTag($"Ground");
        if (checkResult)
        {
            animationContoller.TriggerJumpAnimation(false);
        }
        else
        {
            animationContoller.TriggerJumpAnimation(true);
        }
        
        return checkResult;
    }

    private void Attack()
    {
        SetAttackStatus(true);
        animationContoller.TriggerAttackAnimation();
    }

    public void SetAttackStatus(bool status)
    {
        _attacking = status;
    }

    public bool GetAttackStatus()
    {
        return _attacking;
    }

}
