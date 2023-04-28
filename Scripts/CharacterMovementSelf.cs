using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSelf : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anima;
    public SpriteRenderer sprite;
    private BoxCollider2D collide;
    float Horizontal_movement = 0f;
    [SerializeField] private float move_speed = 9f;
    [SerializeField] private float jump_force = 14f;
    [SerializeField] private LayerMask groundJump;
    private enum MovingState { idle, run, jump, fall}
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_force); 
        }

        Horizontal_movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Horizontal_movement * move_speed, rb.velocity.y);

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovingState state;

        if (Horizontal_movement > 0f)
        {
            state = MovingState.run;
            sprite.flipX = false;
        }

        else if (Horizontal_movement < 0f)
        {
            state = MovingState.run;
            sprite.flipX = true;
        }
        else
        {
            state = MovingState.idle;
        }

        if (rb.velocity.y> 0.1f)
        {
            state = MovingState.jump;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovingState.fall;
        }

        anima.SetInteger("state", (int)state);
    }

    private bool Grounded()
    {
       return Physics2D.BoxCast(collide.bounds.center, collide.bounds.size, 0f, Vector2.down, 0.1f, groundJump);
    }
}
