using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private protected Player_Animation_Contoller animationController;
    [SerializeField] private int _health;
    private int _gems;
    private bool _canBeAttacked;

    public int Health { get; set; }

    private Rigidbody2D _playerRigidbody;

    private bool _attacking;

    private void Start()
    {
        _canBeAttacked = true;
        Health = _health; // this is the only place _health is used. Provides default starting health from the inspector. 
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
        if (!animationController.CanAttackFromAnimation()) return;
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
        if (!_canBeAttacked) return;
        Health -= damageAmount;
        animationController.TriggerDamagedAnimation();
        if (Health < 1)
        {
            Destroy(transform.parent.gameObject);
        }
        _canBeAttacked = false;
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

    public void AddGems(int value)
    {
        _gems += value;
    }
    
    public void RemoveGems(int value)
    {
        _gems -= value;
    }

    public int GetCurrentGems()
    {
        return _gems;
    }

}
