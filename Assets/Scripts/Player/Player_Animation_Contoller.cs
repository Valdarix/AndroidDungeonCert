using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Contoller : MonoBehaviour
{
    [SerializeField] private Animator effectsAnimator;
    [SerializeField] private SpriteRenderer effectsRenderer;
    
    private Animator _animator;
    private SpriteRenderer _renderer;
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The player sprite is missing an Animator Component");
        }

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = false;
            effectsRenderer.flipX = false;
            effectsRenderer.flipY = false;

            var newPos = effectsRenderer.transform.localPosition;
            newPos.x = 0.515f;
            effectsRenderer.transform.localPosition = newPos;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = true;
            effectsRenderer.flipX = true;
            effectsRenderer.flipY = true;
            
            var newPos = effectsRenderer.transform.localPosition;
            newPos.x = -0.515f;
            effectsRenderer.transform.localPosition = newPos;
        }
        _animator.SetFloat(MovementSpeed, Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.GetComponent<Player>().GetAttackStatus())
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("I hit the bad guy!");
            }
        }
    }

    public void TriggerJumpAnimation(bool enable)
    {
        _animator.SetBool(Jump, enable);
    }

    public void TriggerAttackAnimation()
    {
        effectsAnimator.SetTrigger(Attack);
        _animator.SetTrigger(Attack);
    }
}
