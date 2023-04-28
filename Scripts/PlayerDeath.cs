using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anima;
    public GameObject charObject;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpikeDeath"))
        {
            Dead();
        }
        else if (collision.gameObject.CompareTag("FallDeath"))
        {
            
            Dead();
        }
    }
    private void Dead()
    {
        charObject.GetComponent<CharacterMovement>().isOver = true;
    }
}
