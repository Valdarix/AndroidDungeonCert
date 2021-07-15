using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private protected Player_Animation_Contoller animationController;
    [SerializeField] private int _health;

    public int Health { get; set; }

    private Rigidbody2D _playerRigidbody;

    private bool _attacking;

    private void Start()
    {
        Health = _health;
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

        if (!IsGrounded()) return;
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
        var position = transform.position;
        var groundCheck = Physics2D.Raycast(position ,Vector2.down, 1f);
        Debug.DrawRay(position,Vector3.down * 1f,Color.green);
        var checkResult =  groundCheck.collider != null && groundCheck.collider.CompareTag($"Ground");
        animationController.TriggerJumpAnimation(!checkResult);

        return checkResult;
    }

    private void Attack()
    {
        SetAttackStatus(true);
        animationController.TriggerAttackAnimation();
    }

    public void SetAttackStatus(bool status)
    {
        _attacking = status;
    }

    public bool GetAttackStatus()
    {
        return _attacking;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        animationController.TriggerDamagedAnimation();
        if (Health < 1)
        {
            //KILL THE PLAYER
        }
    }

}
