using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Animation_Contoller : MonoBehaviour
{
    [SerializeField] private Animator effectsAnimator;
    [SerializeField] private SpriteRenderer effectsRenderer;
    [FormerlySerializedAs("_hitbox")] [SerializeField] private SpriteRenderer hitbox;
    
    private Animator _animator;
    private SpriteRenderer _renderer;
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");


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
    private void Update()
    {
        var effectsRendererTransform = effectsRenderer.transform;
        Debug.Log(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
        {
            const bool shouldFlip = false;
            _renderer.flipX = shouldFlip;
            effectsRenderer.flipX = shouldFlip;
            effectsRenderer.flipY = shouldFlip;
            hitbox.flipX = shouldFlip;
            hitbox.flipY = shouldFlip;

            var newPos = effectsRendererTransform.localPosition;
            newPos.x = 0.515f;
            effectsRendererTransform.localPosition = newPos;
        }
        else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
        {
            const bool shouldFlip = true;
            _renderer.flipX = shouldFlip;
            effectsRenderer.flipX = shouldFlip ;
            effectsRenderer.flipY = shouldFlip ;
            hitbox.flipX = shouldFlip ;
            hitbox.flipY = shouldFlip ;
            
            var newPos = effectsRenderer.transform.localPosition;
            newPos.x = -0.515f;
            effectsRendererTransform.localPosition = newPos;
        }
        _animator.SetFloat(MovementSpeed, Mathf.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!transform.parent.GetComponent<Player>().GetAttackStatus()) return;
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("I hit the bad guy!");
        }
    }

    public void TriggerJumpAnimation(bool enable)
    {
        _animator.SetBool(Jump, enable);
    }

    public void TriggerAttackAnimation()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit")) return; 
        effectsAnimator.SetTrigger(Attack);
        _animator.SetTrigger(Attack);
    }

    public void TriggerDamagedAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public bool CanAttackFromAnimation()
    {
        var stateInfo = !_animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit") && !_animator.GetCurrentAnimatorStateInfo(0).IsTag("Death");

        return stateInfo;
    }
}
