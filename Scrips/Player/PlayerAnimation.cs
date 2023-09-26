using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public PhysicsCheck phC;
    public PlayerControl plC;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        phC = GetComponent<PhysicsCheck>();
        plC = GetComponent<PlayerControl>();

    }

    private void Update()
    {
        SetAnimations();
    }

    private void SetAnimations()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isFloor", phC.isFloor);
        animator.SetBool("isDead", plC.isDead);
        animator.SetBool("isAttack", plC.isAttack);
    }

    public void PlayHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void PlayAttack(){
        animator.SetTrigger("attack");
    }

}
