using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anima;
    public SpriteRenderer sprite;
    private BoxCollider2D collide;
    float Horizontal_movement = 1f;
    [SerializeField] private float move_speed = 9f;
    [SerializeField] private float jump_force = 14f;
    [SerializeField] private LayerMask groundJump;
    public DNA agent;
    private int index=0;
    private enum MovingState { idle, run, jump, fall}
    private int FPSControl=15;
    public bool isOver = false;
    public GameObject targetObj;
   
    private void Start()
    {
        // agent = new DNA(1000);
        index = 0;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(!isOver)
        {
            if(index<agent.Genes.Length*FPSControl)
            {
                agent.FitnessValue = transform.position.x;
                if(agent.FitnessValue<0)
                {
                    agent.FitnessValue = -1*agent.FitnessValue;
                }
                if(index%FPSControl==0)
                {
                    if (agent.Genes[index/FPSControl]==0 && Grounded())
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump_force); 
                    }
                    if(agent.Genes[index/FPSControl]==1)
                    {
                        rb.velocity = new Vector2(move_speed, rb.velocity.y);   
                    }
                    UpdateAnimationState();
                }
                index += 1;
            }
        }
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

