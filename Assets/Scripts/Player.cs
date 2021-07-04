using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    
    private Rigidbody2D _playerRigidbody;

    [SerializeField] private bool _isGrounded;

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
        
        if (!Input.GetButton("Jump")) return;

        CheckPlayerGrounded();
       
        if (_isGrounded)
            Jump();
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        _isGrounded = false;
        _playerRigidbody.velocity = new Vector2( 0, jumpPower);
    }

    private void CheckPlayerGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);

        if (groundCheck.collider == null) return;
        
        if (groundCheck.collider.CompareTag("Ground"))
        {
            _isGrounded = true; 
        }
    }

   
}
